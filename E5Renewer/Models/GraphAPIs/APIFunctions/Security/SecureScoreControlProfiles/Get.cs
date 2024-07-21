using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs.APIFunctions.Security.SecureScoreControlProfiles
{
    /// <summary>Security.SecureScoreControlProfiles.Get api implementation.</summary>
    public class Get : IAPIFunction
    {
        /// <inheritdoc/>
        public ILogger logger { get; }

        /// <summary>Initialize <see cref="Get"/> class.</summary>
        /// <param name="logger">The <see cref="ILogger{IAPIFunction}"/> implementation.</param>
        /// <remarks>All params should be injected by Asp.Net Core.</remarks>
        public Get(ILogger<Get> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public string id { get => "Security.SecureScoreControlProfiles.Get"; }

        /// <inheritdoc/>
        public async Task<object?> CallAsync(GraphServiceClient client) => await client.Security.SecureScoreControlProfiles.GetAsync();
    }
}
