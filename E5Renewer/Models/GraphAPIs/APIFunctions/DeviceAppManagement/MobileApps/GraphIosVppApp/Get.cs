using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs.APIFunctions.DeviceAppManagement.MobileApps.GraphIosVppApp
{
    /// <summary>DeviceAppManagement.MobileApps.GraphIosVppApp.Get api implementation.</summary>
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
        public string id { get => "DeviceAppManagement.MobileApps.GraphIosVppApp.Get"; }

        /// <inheritdoc/>
        public async Task<object?> CallAsync(GraphServiceClient client) => await client.DeviceAppManagement.MobileApps.GraphIosVppApp.GetAsync();
    }
}
