using Microsoft.AspNetCore.Mvc;
using E5Renewer.Processor;

namespace E5Renewer.Statistics
{
    [ApiController]
    [Route("v1")]
    public class JsonAPIV1Controller : ControllerBase
    {
        private readonly ILogger<JsonAPIV1Controller> logger;
        public JsonAPIV1Controller(ILogger<JsonAPIV1Controller> logger)
        {
            this.logger = logger;
        }

        [HttpGet("list_apis")]
        public InvokeResult GetListApis()
        {
            List<string> result = APIHelper.GetAPIFunctions().Keys.ToList();
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return new(
                "list_apis",
                new(),
                result,
                Helper.GetUnixTimestamp()
            );
        }
        [HttpGet("running_users")]
        public InvokeResult GetRunningUsers()
        {
            List<string> result = UsersManager.GetRunningUsers();
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return new(
                "running_users",
                new(),
                result,
                Helper.GetUnixTimestamp()
            );
        }
        [HttpGet("waiting_users")]
        public InvokeResult GetWaitingUsers()
        {
            List<string> result = UsersManager.GetWaitingUsers();
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return new(
                "waiting_users",
                new(),
                result,
                Helper.GetUnixTimestamp()
            );
        }
        [HttpGet("user_results")]
        public InvokeResult GetUserResults([FromQuery(Name = "user")] string userName, [FromQuery(Name = "api_name")] string apiName)
        {
            List<string> result = ResultsManager.GetResults(userName, apiName);
            logger.LogDebug("Getting result [{0}]", string.Join(", ", result));
            return new(
                "user_results",
                new Dictionary<string, object?>()
                {
                    { "user", userName }, { "api_name", apiName }
                },
                result,
                Helper.GetUnixTimestamp()
            );
        }
    }
}
