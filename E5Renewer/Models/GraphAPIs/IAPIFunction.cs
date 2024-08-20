using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>The api interface of msgraph apis implementations.</summary>
    public interface IAPIFunction
    {
        /// <value>The id of api.</value>
        public string id { get; }

        /// <value>The logger of api.</value>
        public ILogger logger { get; }

        /// <summary>Call the api implementation.</summary>
        /// <param name="client">The <see cref="GraphServiceClient"/> to user.</param>
        /// <remarks>
        /// Do <b>NOT</b> use this directly,
        /// use <see cref="SafeCallAsync(GraphServiceClient, string)"/> instead.
        /// </remarks>
        public Task<object?> CallAsync(GraphServiceClient client);

        /// <summary>Cal the api implementation safely.</summary>
        /// <param name="client">The <see cref="GraphServiceClient"/> to user.</param>
        /// <param name="user">The name of user</param>
        /// <remarks>Errors will be handed correctly here.</remarks>
        internal async Task<APICallResult> SafeCallAsync(GraphServiceClient client, string user)
        {
            object? resultRaw;
            this.logger.LogInformation("Calling for user {0}...", user);
            try
            {
                resultRaw = await this.CallAsync(client);
                this.logger.LogDebug("{0} is {1}", nameof(resultRaw), resultRaw);
                return new(rawResult: resultRaw);
            }
            catch (ODataError oe)
            {
                this.logger.LogError("Failed to send request.");
                this.logger.LogDebug("The error message is `{0}`", oe.Message);
                return new(
                    oe.ResponseStatusCode,
                    oe.Error?.Code ?? "ERROR"
                );
            }
            catch (Exception ex)
            {
                this.logger.LogError("Failed to call implementation.");
                this.logger.LogDebug("The error message is `{0}`", ex.Message);
                return APICallResult.errorResult;
            }
        }
    }
}
