using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>Extended methods to convert normal function to
    /// <see cref="APIFunction">APIFunction</see>.
    /// </summary>
    public static class FuncExtends
    {
        /// <summary>Convert nromal function to
        /// <see cref="APIFunction">APIFunction</see>.
        /// </summary>
        /// <param name="method">The method to convert.</param>
        /// <param name="id">The id of msgraph api.</param>
        /// <param name="logger">The logger to generate log.</param>
        public static KeyValuePair<string, APIFunction> ToAPIFunction(Func<GraphServiceClient, Task<object?>> method, string id, ILogger logger)
        {
            return new(id,
                       async (client) =>
                    {
                        logger.LogDebug("Calling msgraph api {0}", id);
                        try
                        {
                            object? rawResult = await method(client);
                            logger.LogDebug("Call msgraph result is {0}", rawResult);
                            return new(rawResult: rawResult);
                        }
                        catch (ODataError ode)
                        {
                            logger.LogError("Failed to send request because {0}", ode.Message);
                            return new APICallResult(
                                ode.ResponseStatusCode,
                                ode.Error?.Code ?? "ERROR"
                            );
                        }
                        catch (Exception ex)
                        {
                            logger.LogError("Failed to call msgraph api because {0}", ex.Message);
                            return APICallResult.errorResult;
                        }
                    }
            );
        }
    }
}
