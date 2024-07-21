using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs.APIFunctions.RoleManagement.Directory.RoleEligibilitySchedules.Count
{
    /// <summary>RoleManagement.Directory.RoleEligibilitySchedules.Count.Get api implementation.</summary>
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
        public string id { get => "RoleManagement.Directory.RoleEligibilitySchedules.Count.Get"; }

        /// <inheritdoc/>
        public async Task<object?> CallAsync(GraphServiceClient client) => await client.RoleManagement.Directory.RoleEligibilitySchedules.Count.GetAsync();
    }
}
