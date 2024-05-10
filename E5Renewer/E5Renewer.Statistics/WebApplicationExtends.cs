using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace E5Renewer.Statistics
{
    /// <summary>Extends for WebApplication</summary>
    /// <seealso cref="WebApplication"/>
    public static class WebApplicationExtends
    {
        /// <summary>Use custom authentication middleware.</summary>
        /// <param name="app">The WebApplication instance.</param>
        /// <param name="authToken">The token used for authentication.</param>
        public static IApplicationBuilder UseAuthTokenAuthentication(this WebApplication app, string authToken)
        {
            return app.Use(
                async (context, next) =>
                {
                    const string authenticationHeaderName = "Authentication";
                    string? authentication = context.Request.Headers[authenticationHeaderName].FirstOrDefault();
                    if (authentication == authToken)
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Authenticate failed");
                }
            );
        }
        /// <summary>Only allow methods given to connect.</summary>
        /// <param name="app">The WebApplication instance.</param>
        /// <param name="methods">The request methods to allow.</param>
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
                    context.Response.StatusCode = 405;
                    context.Response.Headers["Allow"] = string.Join(", ", methods);
                }
            );
        }
        /// <summary>Check timestamp in request.</summary>
        /// <param name="app">The WebApplication instance.</param>
        /// <param name="allowedMaxSeconds">Max allowed seconds.</param>
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
                    long timestampNow = Helper.GetUnixTimestamp();
                    if (timestamp > 0 && timestampNow > timestamp && timestampNow - timestamp <= allowedMaxSeconds * 1000)
                    {
                        await next();
                        return;
                    }
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Request is outdated");
                }
            );
        }
        private static object? TryConvertTo(this string? input, Type t)
        {
            if (input == null || t.IsInstanceOfType(input))
            {
                return input;
            }
            try
            {
                return Convert.ChangeType(input, t);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static async Task<Dictionary<K, V>> ToDictionary<K, V>(this Stream stream)
            where K : notnull
        {
            return await JsonSerializer.DeserializeAsync<Dictionary<K, V>>(stream) ?? new();
        }
    }
}
