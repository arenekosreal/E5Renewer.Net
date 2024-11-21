using System.Text.Json;

using E5Renewer.Controllers;
using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Modules;
using E5Renewer.Models.Secrets;
using E5Renewer.Models.Statistics;

using Microsoft.Extensions.Primitives;

namespace E5Renewer
{
    internal static class WebApplicationExtends
    {
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
                    const string authenticationHeaderName = "Authentication";
                    string? authentication = context.Request.Headers[authenticationHeaderName].FirstOrDefault();
                    if (authentication == await secretProvider.GetRuntimeTokenAsync())
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    InvokeResult result = await dummyResultGenerator.GenerateDummyResultAsync(context);
                    await context.Response.WriteAsJsonAsync(result);
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
                    const string timestampKey = "timestamp";
                    string timestampValue = context.Request.Method switch
                    {
                        "GET" => context.Request.Query.TryGetValue(timestampKey, out StringValues value) ? value.FirstOrDefault() ?? "" : "",
                        "POST" => (await context.Request.Body.ToDictionary<string, string>()).TryGetValue(timestampKey, out string? value) ? value ?? "" : "",
                        _ => ""
                    };
                    if (!long.TryParse(timestampValue, out long timestamp))
                    {
                        timestamp = -1;
                    }
                    long timestampNow = app.Services.GetRequiredService<IUnixTimestampGenerator>().GetUnixTimestamp();
                    if ((timestamp > 0) && (timestampNow > timestamp) && (timestampNow - timestamp <= allowedMaxSeconds * 1000))
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    InvokeResult result = await app.Services.GetRequiredService<IDummyResultGenerator>().GenerateDummyResultAsync(context);
                    await context.Response.WriteAsJsonAsync(result);
                }
            );
        }

        private static async Task<Dictionary<K, V>> ToDictionary<K, V>(this Stream stream)
            where K : notnull
        {
            return await JsonSerializer.DeserializeAsync<Dictionary<K, V>>(stream) ?? new();
        }
    }
}
