using System.Text.Json.Serialization.Metadata;

using CaseConverter;

using E5Renewer.Controllers;
using E5Renewer.Controllers.V1;
using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Modules;
using E5Renewer.Models.Secrets;
using E5Renewer.Models.Statistics;

using Microsoft.Extensions.Primitives;

namespace E5Renewer
{
    internal static class WebApplicationExtends
    {
        private static readonly JsonTypeInfo<JsonAPIV1Response> responseJsonTypeInfo =
            JsonAPIV1ResponseJsonSerializerContext.Default.JsonAPIV1Response;

        public static IApplicationBuilder UseModulesCheckers(this WebApplication app)
        {
            IEnumerable<IModulesChecker> modulesCheckers = app.Services.GetServices<IModulesChecker>();
            IEnumerable<IUserSecretLoader> userSecretLoaders = app.Services.GetServices<IUserSecretLoader>();
            IEnumerable<IGraphAPICaller> graphAPICallers = app.Services.GetServices<IGraphAPICaller>();
            IEnumerable<IModule> otherModules = app.Services.GetServices<IModule>();

            List<IModule> modulesToCheck = new();
            modulesToCheck.AddRange(modulesCheckers);
            modulesToCheck.AddRange(userSecretLoaders);
            modulesToCheck.AddRange(graphAPICallers);
            modulesToCheck.AddRange(otherModules);

            modulesToCheck.ForEach(
                (m) =>
                {
                    foreach (IModulesChecker checker in modulesCheckers)
                    {

                        try
                        {
                            checker.CheckModules(m);
                        }
                        catch { }
                    }
                }
            );

            return app;
        }

        public static IApplicationBuilder UseAuthTokenAuthentication(this WebApplication app)
        {
            return app.Use(
                async (context, next) =>
                {
                    ISecretProvider secretProvider = app.Services.GetRequiredService<ISecretProvider>();
                    IDummyResultGenerator dummyResultGenerator = app.Services.GetRequiredService<IDummyResultGenerator>();
                    const string authorizationScheme = "Bearer";
                    string authorization = context.Request.Headers[nameof(authorization).ToTitleCase()].FirstOrDefault() ?? string.Empty;
                    if (authorization == $"{authorizationScheme.ToTitleCase()} {await secretProvider.GetRuntimeTokenAsync()}")
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    IJsonResponse result = await dummyResultGenerator.GenerateDummyResultAsync(context);
                    await context.Response.WriteAsJsonAsync(result, WebApplicationExtends.responseJsonTypeInfo);
                }
            );
        }

        public static IApplicationBuilder UseHttpMethodChecker(this WebApplication app, params string[] methods)
        {
            return app.Use(
                async (context, next) =>
                {
                    if (methods.Contains(context.Request.Method))
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                    context.Response.Headers["Allow"] = string.Join(", ", methods);
                }
            );
        }

        public static IApplicationBuilder UseUnixTimestampChecker(this WebApplication app, uint allowedMaxSeconds = 30)
        {
            return app.Use(
                async (context, next) =>
                {
                    long timestamp = context.Request.Method switch
                    {
                        "GET" => context.Request.Query.TryGetValue(nameof(timestamp), out StringValues value) &&
                                 long.TryParse(value, out long parsedValue)
                                 ? parsedValue : -1,
                        "POST" => (await context.Request.Body.ToJsonAPIV1RequestAsync()).timestamp,
                        _ => -1
                    };

                    long timestampNow = app.Services.GetRequiredService<IUnixTimestampGenerator>().GetUnixTimestamp();
                    if ((timestamp > 0) && (timestampNow > timestamp) && (timestampNow - timestamp <= allowedMaxSeconds * 1000))
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    IJsonResponse result = await app.Services.GetRequiredService<IDummyResultGenerator>().GenerateDummyResultAsync(context);
                    await context.Response.WriteAsJsonAsync(result, WebApplicationExtends.responseJsonTypeInfo);
                }
            );
        }

    }
}
