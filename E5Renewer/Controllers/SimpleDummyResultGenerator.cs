using System.Collections.ObjectModel;

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
        public async Task<JsonAPIV1Response> GenerateDummyResultAsync(HttpContext httpContext)
        {
            JsonAPIV1Request input = httpContext.Request.Method switch
            {
                "GET" => httpContext.Request.Query.ToJsonAPIV1Request(),
                "POST" => await httpContext.Request.Body.ToJsonAPIV1RequestAsync(),
                _ => new()
            };
            string fullPath = httpContext.Request.PathBase + httpContext.Request.Path;
            int lastOfSlash = fullPath.LastIndexOf("/");
            int firstOfQuote = fullPath.IndexOf("?");
            string methodName =
                firstOfQuote > lastOfSlash ?
                    fullPath.Substring(lastOfSlash + 1, firstOfQuote - lastOfSlash) :
                    fullPath.Substring(lastOfSlash + 1);
            return new(
                this.unixTimestampGenerator.GetUnixTimestamp(),
                methodName,
                null,
                input.args ?? ReadOnlyDictionary<string, string?>.Empty
            );
        }

        /// <inheritdoc/>
        public JsonAPIV1Response GenerateDummyResult(HttpContext httpContext) => this.GenerateDummyResultAsync(httpContext).Result;
    }
}
