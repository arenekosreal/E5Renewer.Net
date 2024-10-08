using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Statistics;

using Microsoft.AspNetCore.Mvc;

namespace E5Renewer.Controllers
{
    /// <summary>Json api controller.</summary>
    [ApiController]
    [Route("v1")]
    public class JsonAPIV1Controller : ControllerBase
    {
        private readonly ILogger<JsonAPIV1Controller> logger;
        private readonly IStatusManager statusManager;
        private readonly IEnumerable<IAPIFunction> apiFunctions;
        private readonly IUnixTimestampGenerator unixTimestampGenerator;

        /// <summary>Initialize controller by logger given.</summary>
        /// <param name="logger">The logger to create logs.</param>
        /// <param name="statusManager">The <see cref="IStatusManager"/> implementation.</param>
        /// <param name="apiFunctions">The <see cref="IAPIFunction"/> implementation.</param>
        /// <param name="unixTimestampGenerator">The <see cref="IUnixTimestampGenerator"/> implementation.</param>
        /// <remarks>All the params are injected by Asp.Net Core runtime.</remarks>
        public JsonAPIV1Controller(
            ILogger<JsonAPIV1Controller> logger,
            IStatusManager statusManager,
            IEnumerable<IAPIFunction> apiFunctions,
            IUnixTimestampGenerator unixTimestampGenerator
        )
        {
            this.logger = logger;
            this.statusManager = statusManager;
            this.apiFunctions = apiFunctions;
            this.unixTimestampGenerator = unixTimestampGenerator;
        }

        /// <summary>Handler for <c>/v1/list_apis</c>.</summary>
        [HttpGet("list_apis")]
        public ValueTask<InvokeResult> GetListApis()
        {
            IEnumerable<string> result = this.apiFunctions.
                Select((c) => c.id);
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return ValueTask.FromResult(new InvokeResult(
                "list_apis",
                new(),
                result,
                this.unixTimestampGenerator.GetUnixTimestamp()
            ));
        }

        /// <summary>Handler for <c>/v1/running_users</c>.</summary>
        [HttpGet("running_users")]
        public async ValueTask<InvokeResult> GetRunningUsers()
        {
            IEnumerable<string> result = await this.statusManager.GetRunningUsersAsync();
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return new(
                "running_users",
                new(),
                result,
                this.unixTimestampGenerator.GetUnixTimestamp()
            );
        }

        /// <summary>Handler for <c>/v1/waiting_users</c>.</summary>
        [HttpGet("waiting_users")]
        public async ValueTask<InvokeResult> GetWaitingUsers()
        {
            IEnumerable<string> result = await this.statusManager.GetWaitingUsersAsync();
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return new(
                "waiting_users",
                new(),
                result,
                this.unixTimestampGenerator.GetUnixTimestamp()
            );
        }

        /// <summary>Handler for <c>/v1/user_results</c>.</summary>
        [HttpGet("user_results")]
        public async ValueTask<InvokeResult> GetUserResults(
            [FromQuery(Name = "user")]
            string userName,

            [FromQuery(Name = "api_name")]
            string apiName
        )
        {
            IEnumerable<string> result = await this.statusManager.GetResultsAsync(userName, apiName);
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return new(
                "user_results",
                new Dictionary<string, object?>()
                {
                    { "user", userName }, { "api_name", apiName }
                },
                result,
                this.unixTimestampGenerator.GetUnixTimestamp()
            );
        }
    }
}
