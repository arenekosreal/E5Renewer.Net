using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

using E5Renewer.Models.Statistics;

namespace E5Renewer.Controllers
{
    /// <summary>Generate a dummy result when something is not right.</summary>
    public class SimpleDummyResultGenerator : IDummyResultGenerator
    {
        private readonly ILogger<SimpleDummyResultGenerator> logger;
        private readonly IUnixTimestampGenerator unixTimestampGenerator;

        /// <summary>Initialize <see cref="SimpleDummyResultGenerator"/> with arguments given.</summary>
        /// <param name="logger">The logger to generate log.</param>
        /// <param name="unixTimestampGenerator">The <see cref="IUnixTimestampGenerator"/> implementation.</param>
        /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
        public SimpleDummyResultGenerator(ILogger<SimpleDummyResultGenerator> logger, IUnixTimestampGenerator unixTimestampGenerator) =>
            (this.logger, this.unixTimestampGenerator) = (logger, unixTimestampGenerator);

        /// <inheritdoc/>
        [RequiresUnreferencedCode("Calls JsonSerializer.Deserialize<T>(ReadOnlySpan<byte>)")]
        public async Task<InvokeResult> GenerateDummyResultAsync(HttpContext httpContext)
        {
            Dictionary<string, object?> queries;
            switch (httpContext.Request.Method)
            {
                case "GET":
                    queries = httpContext.Request.Query.Select(
                        (kv) => new KeyValuePair<string, object?>(kv.Key, kv.Value.FirstOrDefault() as object)
                    ).ToDictionary();
                    break;
                case "POST":
                    byte[] buffer = new byte[httpContext.Request.ContentLength ?? httpContext.Request.Body.Length];
                    int length = await httpContext.Request.Body.ReadAsync(buffer);
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
            string fullPath = httpContext.Request.PathBase + httpContext.Request.Path;
            int lastOfSlash = fullPath.LastIndexOf("/");
            int firstOfQuote = fullPath.IndexOf("?");
            string methodName =
                firstOfQuote > lastOfSlash ?
                    fullPath.Substring(lastOfSlash + 1, firstOfQuote - lastOfSlash) :
                    fullPath.Substring(lastOfSlash + 1);
            return new(methodName,
                    queries,
                    null,
                    this.unixTimestampGenerator.GetUnixTimestamp()
            );
        }

        /// <inheritdoc/>
        public InvokeResult GenerateDummyResult(HttpContext httpContext) => this.GenerateDummyResultAsync(httpContext).Result;
    }
}
