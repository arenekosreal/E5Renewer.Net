using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs.APIFunctions.RoleManagement.EntitlementManagement.RoleEligibilitySchedules.Count
{
    /// <summary>RoleManagement.EntitlementManagement.RoleEligibilitySchedules.Count.Get api implementation.</summary>
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
        public string id { get => "RoleManagement.EntitlementManagement.RoleEligibilitySchedules.Count.Get"; }

        /// <inheritdoc/>
        public async Task<object?> CallAsync(GraphServiceClient client) => await client.RoleManagement.EntitlementManagement.RoleEligibilitySchedules.Count.GetAsync();
    }
}
