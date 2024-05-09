using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace E5Renewer.Processor
{
    public static class APIHelper
    {
        private static readonly ILogger logger = LoggerFactory.Create(
            (build) => build.AddSimpleConsole(
                (options) =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = E5Renewer.Constraints.loggingTimeFormat;
                }
            ).SetMinimumLevel(E5Renewer.Constraints.loggingLevel)).CreateLogger(typeof(APIHelper));
        private static readonly Dictionary<string, APIFunction> cachedFunctions = new();
        private static readonly Dictionary<Type, Dictionary<string, APIFunction>> cachedFunctionsMap = new();
        public delegate Task<APICallResult> APIFunction(GraphServiceClient client);
        public static Dictionary<string, APIFunction> GetAPIFunctions()
        {
            if (cachedFunctions.Count() > 0)
            {
                logger.LogDebug("Using cached value for api functions");
                return cachedFunctions;
            }
            Dictionary<string, APIFunction> results = new();
            const string ns = "E5Renewer.Processor.GraphAPIs";
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(
                (t) => t.Namespace == ns && t.IsDefined(typeof(APIContainerAttribute))
            );
            foreach (Type t in types)
            {
                foreach (KeyValuePair<string, APIFunction> kv in GetAPIFunctions(t))
                {
                    results[kv.Key] = kv.Value;
                }
            }
            foreach (KeyValuePair<string, APIFunction> kv in results)
            {
                cachedFunctions[kv.Key] = kv.Value;
            }
            return results;
        }
        public static Dictionary<string, APIFunction> GetAPIFunctions(Type type)
        {
            if (cachedFunctionsMap.ContainsKey(type))
            {
                logger.LogDebug("Using cached value for api functions");
                return cachedFunctionsMap[type];
            }
            Dictionary<string, APIFunction> result = new();
            IEnumerable<MethodInfo> methodInfos = type.GetMethods(BindingFlags.Static | BindingFlags.Public).TakeWhile(
                (methodInfo) =>
                methodInfo.IsDefined(typeof(APIAttribute)) &&
                    methodInfo.IsDefined(typeof(AsyncStateMachineAttribute)) &&
                    methodInfo.GetParameters().Count() == 1 &&
                    methodInfo.GetParameters()[0].ParameterType == typeof(GraphServiceClient)

            );
            foreach (MethodInfo methodInfo in methodInfos)
            {
                string? name = methodInfo.GetCustomAttribute<APIAttribute>()?.name;
                if (name != null)
                {
                    result.Add(
                        name,
                        async delegate (GraphServiceClient client)
                        {
                            logger.LogDebug("Calling API {0}...", name);
                            try
                            {
                                dynamic? methodResult = methodInfo.Invoke(null, new object[1] { client });
                                if (methodResult != null)
                                {
                                    object? o = await methodResult;
                                    logger.LogDebug("Result is {0}", o);
                                    return new(200, "OK", o);
                                }
                                else
                                {
                                    logger.LogWarning("Task is null");
                                    logger.LogDebug("Actual type is {0}", (object?)methodResult?.GetType());
                                    return APICallResult.errorResult;
                                }
                            }
                            catch (ODataError oe)
                            {
                                return new APICallResult(
                                    oe.ResponseStatusCode,
                                    oe.Error?.Code ?? "ERROR"
                                );
                            }
                            catch (Exception e)
                            {
                                logger.LogDebug("Failed to send request because {0}", e);
                                return APICallResult.errorResult;
                            }
                        }
                    );
                }
            }
            cachedFunctionsMap[type] = result;
            return result;

        }
    }
}
