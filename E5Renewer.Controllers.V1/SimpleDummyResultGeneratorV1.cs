using System.Collections.ObjectModel;

using E5Renewer.Models.Statistics;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace E5Renewer.Controllers.V1;

/// <summary>Generate a dummy result when something is not right.</summary>
public class SimpleDummyResultGeneratorV1 : IDummyResultGenerator
{
    private readonly ILogger<SimpleDummyResultGeneratorV1> logger;
    private readonly IUnixTimestampGenerator unixTimestampGenerator;

    /// <summary>Initialize <see cref="SimpleDummyResultGeneratorV1"/> with arguments given.</summary>
    /// <param name="logger">The logger to generate log.</param>
    /// <param name="unixTimestampGenerator">The <see cref="IUnixTimestampGenerator"/> implementation.</param>
    /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
    public SimpleDummyResultGeneratorV1(ILogger<SimpleDummyResultGeneratorV1> logger, IUnixTimestampGenerator unixTimestampGenerator) =>
        (this.logger, this.unixTimestampGenerator) = (logger, unixTimestampGenerator);

    /// <inheritdoc/>
    public async Task<IJsonResponse> GenerateDummyResultAsync(HttpContext httpContext)
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
        return new JsonAPIV1Response(
            this.unixTimestampGenerator.GetUnixTimestamp(),
            methodName,
            null,
            input.args ?? ReadOnlyDictionary<string, string?>.Empty
        );
    }

    /// <inheritdoc/>
    public IJsonResponse GenerateDummyResult(HttpContext httpContext) => this.GenerateDummyResultAsync(httpContext).Result;
}

