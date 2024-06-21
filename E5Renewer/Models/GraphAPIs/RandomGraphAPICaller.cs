using Azure.Identity;

using E5Renewer.Models.Config;
using E5Renewer.Models.Modules;
using E5Renewer.Models.Statistics;

using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>
    /// <see cref="IGraphAPICaller">IGraphAPICaller</see>
    /// implementation which calls msgraph apis randomly.
    /// </summary>
    [Module]
    public class RandomGraphAPICaller : IGraphAPICaller
    {
        private readonly ILogger<RandomGraphAPICaller> logger;
        private readonly IEnumerable<IAPIFunctionsContainer> apiFunctions;
        private readonly IStatusManager statusManager;
        private readonly Dictionary<GraphUser, GraphServiceClient> clients = new();

        /// <summary>Initialize <c>RandomGraphAPICaller</c> with parameters given.</summary>
        /// <param name="logger">The logger to generate log.</param>
        /// <param name="apiFunctions">All known api functions with their id.</param>
        /// <param name="statusManager"><see cref="IStatusManager">IStatusManager</see> implementation.</param>
        /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
        public RandomGraphAPICaller(
            ILogger<RandomGraphAPICaller> logger,
            IEnumerable<IAPIFunctionsContainer> apiFunctions,
            IStatusManager statusManager
        )
        {
            this.logger = logger;
            this.apiFunctions = apiFunctions;
            this.statusManager = statusManager;
        }

        /// <inheritdoc/>
        public string name { get => nameof(RandomGraphAPICaller); }

        /// <inheritdoc/>
        public string author { get => "E5Renewer"; }

        /// <inheritdoc/>
        public SemanticVersioning.Version apiVersion
        {
            get => typeof(RandomGraphAPICaller).Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
        }

        /// <inheritdoc/>
        public async Task CallNextAPIAsync(GraphUser user)
        {
            if (!this.clients.ContainsKey(user))
            {
                ClientCertificateCredentialOptions options = new()
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };
                ClientSecretCredential credential = new(user.tenantId, user.clientId, user.secret, options);
                GraphServiceClient client = new(credential, ["https://graph.microsoft.com/.default"]);
                this.clients[user] = client;
            }
            Random random = new();
            if (this.apiFunctions.Count() <= 0)
            {
                this.logger.LogError("No IAPIFunctionsContainer is found.");
                return;
            }
            IAPIFunctionsContainer container = random.GetItems(this.apiFunctions.ToArray(), 1)[0];
            this.logger.LogDebug("Using IAPIFunctionsContainer {0}", container.GetType().Name);
            IEnumerable<KeyValuePair<string, APIFunction>> apiFunctions = container.GetAPIFunctions();
            if (apiFunctions.Count() <= 0)
            {
                this.logger.LogError("No KeyValuePair<string, APIFunction> is found.");
                return;
            }
            KeyValuePair<string, APIFunction> apiFunction = random.GetItems(apiFunctions.ToArray(), 1)[0];
            APICallResult result = await apiFunction.Value(this.clients[user]);
            await this.statusManager.SetResultAsync(user.name, apiFunction.Key, result.ToString());
        }

        /// <inheritdoc/>
        public async Task CalmDownAsync(CancellationToken token, GraphUser user)
        {
            if (user.enabled)
            {
                const uint calmDownMinMilliSeconds = 300000;
                const uint calmDownMaxMilliSeconds = 500000;
                Random random = new();
                int milliseconds = random.Next((int)calmDownMinMilliSeconds, (int)calmDownMaxMilliSeconds);
                this.logger.LogDebug("{0} is going to sleep for {1} millisecond(s)", user.name, milliseconds);
                await Task.Delay(milliseconds, token);
            }
        }
    }
}
