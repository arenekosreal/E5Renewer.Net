using E5Renewer.Config;
using E5Renewer.Exceptions;
using E5Renewer.Modules;

namespace E5Renewer.Processor
{
    internal static class GraphUserExtends
    {
        private static readonly ILogger logger = LoggerFactory.Create(
            (build) => build.AddSimpleConsole(
                (options) =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = E5Renewer.Constraints.loggingTimeFormat;
                }
            ).SetMinimumLevel(E5Renewer.Constraints.loggingLevel)
        ).CreateLogger(typeof(GraphUserExtends));
        private static readonly Dictionary<GraphUser, IAPICaller> apiCallerMap = new();
        public static IAPICaller GetAPICaller(this GraphUser user)
        {
            if (apiCallerMap.ContainsKey(user))
            {
                logger.LogDebug("Using apiCaller in cache");
                return apiCallerMap[user];
            }
            IAPICaller[] apiCallers = ModulesLoader.GetRegisteredModules<IAPICaller>().ToArray();
            Random random = new();
            if (apiCallers.Count() > 0)
            {
                IAPICaller apiCaller = random.GetItems(apiCallers, 1)[0];
                apiCaller.AddUser(user);
                apiCallerMap[user] = apiCaller;
                return apiCaller;
            }
            throw new RuntimeException("No API Caller is found");
        }
    }
}
