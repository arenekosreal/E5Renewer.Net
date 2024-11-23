
using System.Collections.ObjectModel;

using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Statistics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E5Renewer.Controllers.V1;

/// <summary>Json api controller.</summary>
[ApiController]
[Route("v1")]
public class JsonAPIV1Controller : ControllerBase
{
    private readonly ILogger<JsonAPIV1Controller> logger;
    private readonly IStatusManager statusManager;
    private readonly IEnumerable<IAPIFunction> apiFunctions;
    private readonly IUnixTimestampGenerator unixTimestampGenerator;
    private readonly IDummyResultGenerator dummyResponseGenerator;

    /// <summary>Initialize controller by logger given.</summary>
    /// <param name="logger">The logger to create logs.</param>
    /// <param name="statusManager">The <see cref="IStatusManager"/> implementation.</param>
    /// <param name="apiFunctions">The <see cref="IAPIFunction"/> implementation.</param>
    /// <param name="unixTimestampGenerator">The <see cref="IUnixTimestampGenerator"/> implementation.</param>
    /// <param name="dummyResponseGenerator">The <see cref="IDummyResultGenerator"/> implementation.</param>
    /// <remarks>All the params are injected by Asp.Net Core runtime.</remarks>
    public JsonAPIV1Controller(
        ILogger<JsonAPIV1Controller> logger,
        IStatusManager statusManager,
        IEnumerable<IAPIFunction> apiFunctions,
        IUnixTimestampGenerator unixTimestampGenerator,
        IDummyResultGenerator dummyResponseGenerator
    ) =>
        (this.logger, this.statusManager, this.apiFunctions, this.unixTimestampGenerator, this.dummyResponseGenerator) =
        (logger, statusManager, apiFunctions, unixTimestampGenerator, dummyResponseGenerator);

    /// <summary>Handler for <c>/v1/list_apis</c>.</summary>
    [HttpGet("list_apis")]
    public ValueTask<JsonAPIV1Response> GetListApis()
    {
        IEnumerable<string> result = this.apiFunctions.
            Select((c) => c.id);
        logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
        return ValueTask.FromResult(new JsonAPIV1Response(
            this.unixTimestampGenerator.GetUnixTimestamp(),
            "list_apis",
            result,
            ReadOnlyDictionary<string, string?>.Empty
        ));
    }

    /// <summary>Handler for <c>/v1/running_users</c>.</summary>
    [HttpGet("running_users")]
    public async ValueTask<JsonAPIV1Response> GetRunningUsers()
    {
        IEnumerable<string> result = await this.statusManager.GetRunningUsersAsync();
        logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
        return new JsonAPIV1Response(
            this.unixTimestampGenerator.GetUnixTimestamp(),
            "running_users",
            result,
            ReadOnlyDictionary<string, string?>.Empty
        );
    }

    /// <summary>Handler for <c>/v1/waiting_users</c>.</summary>
    [HttpGet("waiting_users")]
    public async ValueTask<JsonAPIV1Response> GetWaitingUsers()
    {
        IEnumerable<string> result = await this.statusManager.GetWaitingUsersAsync();
        logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
        return new JsonAPIV1Response(
            this.unixTimestampGenerator.GetUnixTimestamp(),
            "waiting_users",
            result,
            ReadOnlyDictionary<string, string?>.Empty
        );
    }

    /// <summary>Handler for <c>/v1/user_results</c>.</summary>
    [HttpGet("user_results")]
    public async ValueTask<JsonAPIV1Response> GetUserResults(
        [FromQuery(Name = "user")]
            string userName,

        [FromQuery(Name = "api_name")]
            string apiName
    )
    {
        IEnumerable<string> result = await this.statusManager.GetResultsAsync(userName, apiName);
        logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
        Dictionary<string, string?> queries = new()
            {
                {"user", userName},{"api_name", apiName}
            };
        return new JsonAPIV1Response(
            this.unixTimestampGenerator.GetUnixTimestamp(),
            "user_results",
            result,
            new(queries)
        );
    }

    /// <summary>Handler for <c>/v1/*</c>.</summary>
    [Route("{*method}")]
    public async ValueTask<JsonAPIV1Response> Handle() =>
        await this.dummyResponseGenerator.GenerateDummyResultAsync<JsonAPIV1Response>(this.HttpContext);
}

