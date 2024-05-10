using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace E5Renewer.Statistics
{
    /// <summary>Utils to handle api request exceptions.</summary>
    public static class ExceptionHandlers
    {
        private static ILogger logger = LoggerFactory.Create(
            (options) => options.AddSimpleConsole(
                    (options) =>
                    {
                        options.SingleLine = true;
                        options.TimestampFormat = E5Renewer.Constraints.loggingTimeFormat;
                    }
                ).SetMinimumLevel(E5Renewer.Constraints.loggingLevel)
        ).CreateLogger(typeof(ExceptionHandlers));
        /// <summary>Generate an exception result from ActionContext.</summary>
        /// <seealso cref="ActionContext"/>
        /// <returns>The exception result.</returns>
        public static async Task<ObjectResult> GenerateExceptionResult(ActionContext context) => await GenerateExceptionResult(context.HttpContext);
        /// <summary>Generate an exception result from HttpContext.</summary>
        /// <seealso cref="HttpContext"/>
        /// <returns>The exception result.</returns>
        public static async Task<ObjectResult> GenerateExceptionResult(HttpContext context)
        {
            logger.LogWarning("Creating an empty InvokeResult to hide exceptions");
            Dictionary<string, object?> queries;
            switch (context.Request.Method)
            {
                case "GET":
                    queries = context.Request.Query.Select(
                        (kv) => new KeyValuePair<string, object?>(kv.Key, kv.Value.FirstOrDefault() as object)
                    ).ToDictionary();
                    break;
                case "POST":
                    byte[] buffer = new byte[context.Request.ContentLength ?? context.Request.Body.Length];
                    int length = await context.Request.Body.ReadAsync(buffer);
                    byte[] contents = buffer.Take(length).ToArray();
                    queries = JsonSerializer.Deserialize<Dictionary<string, object?>>(contents) ?? new();
                    break;
                default:
                    queries = new();
                    break;
            }
            if (queries.ContainsKey("timestamp"))
            {
                queries.Remove("timestamp");
            }
            string fullPath = context.Request.PathBase + context.Request.Path;
            int lastOfSlash = fullPath.LastIndexOf("/");
            int firstOfQuote = fullPath.IndexOf("?");
            string methodName =
                firstOfQuote > lastOfSlash ?
                    fullPath.Substring(lastOfSlash + 1, firstOfQuote - lastOfSlash) :
                    fullPath.Substring(lastOfSlash + 1);
            return new ObjectResult(
                new InvokeResult(
                    methodName,
                    queries,
                    null,
                    E5Renewer.Statistics.Helper.GetUnixTimestamp()
                )
            );
        }
    }
}
