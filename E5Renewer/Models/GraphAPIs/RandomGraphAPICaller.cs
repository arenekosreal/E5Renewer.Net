using System.Security.Cryptography.X509Certificates;

using Azure.Core;
using Azure.Identity;

using E5Renewer.Models.Modules;
using E5Renewer.Models.Secrets;
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
        private readonly IEnumerable<IAPIFunction> apiFunctions;
        private readonly IStatusManager statusManager;
        private readonly ISecretProvider secretProvider;
        private readonly Dictionary<User, GraphServiceClient> clients = new();

        /// <summary>Initialize <c>RandomGraphAPICaller</c> with parameters given.</summary>
        /// <param name="logger">The logger to generate log.</param>
        /// <param name="apiFunctions">All known api functions with their id.</param>
        /// <param name="statusManager"><see cref="IStatusManager"/> implementation.</param>
        /// <param name="secretProvider"><see cref="ISecretProvider"/> implementations.</param>
        /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
        public RandomGraphAPICaller(
            ILogger<RandomGraphAPICaller> logger,
            IEnumerable<IAPIFunction> apiFunctions,
            IStatusManager statusManager,
            ISecretProvider secretProvider
        )
        {
            this.logger = logger;
            this.apiFunctions = apiFunctions;
            this.statusManager = statusManager;
            this.secretProvider = secretProvider;
            this.logger.LogDebug("Found {0} api functions", this.apiFunctions.Count());
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
        public async Task CallNextAPIAsync(User user)
        {
            if (!this.clients.ContainsKey(user))
            {
                ClientCertificateCredentialOptions options = new()
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };
                TokenCredential credential;
                if (user.certificate?.Exists ?? false)
                {
                    this.logger.LogDebug("Using certificate to get user token.");
                    string? password = await this.secretProvider.GetPasswordForCertificateAsync(user.certificate);
                    if (password is not null)
                    {
                        this.logger.LogDebug("Found password for certificate given.");
                    }
                    using (FileStream fileStream = user.certificate.OpenRead())
                    {
                        byte[] buffer = new byte[user.certificate.Length];
                        int size = await fileStream.ReadAsync(buffer);
                        X509Certificate2 certificate = new(buffer.Take(size).ToArray(), password);
                        credential = new ClientCertificateCredential(user.tenantId, user.clientId, certificate, options);
                    }
                }
                else if (!string.IsNullOrEmpty(user.secret))
                {
                    this.logger.LogDebug("Using secret to get user token.");
                    credential = new ClientSecretCredential(user.tenantId, user.clientId, user.secret, options);
                }
                else
                {
                    throw new NullReferenceException($"{nameof(user.certificate)} and {nameof(user.secret)} are both invalid.");
                }
                GraphServiceClient client = new(credential, ["https://graph.microsoft.com/.default"]);
                this.clients[user] = client;
            }

            if (this.apiFunctions.Count() <= 0)
            {
                this.logger.LogError("No {0} is found.", nameof(IAPIFunction));
                return;
            }


            int GetFunctionWeightOfCurrentUser(IAPIFunction function)
            {
                string successAPICallResult = new APICallResult().ToString();
                IEnumerable<string> results = this.statusManager.GetResultsAsync(user.name, function.id).Result;
                int successCount = results.Count((item) => item == successAPICallResult);
                return successCount + 1; // let weight greater than zero
            }

            IAPIFunction apiFunction = this.apiFunctions.GetDifferentItemsByWeight(GetFunctionWeightOfCurrentUser, 1).First();
            APICallResult result = await apiFunction.SafeCallAsync(this.clients[user], user.name);
            await this.statusManager.SetResultAsync(user.name, apiFunction.id, result.ToString());
        }

        /// <inheritdoc/>
        public async Task CalmDownAsync(CancellationToken token, User user)
        {
            if (user.timeToStart == TimeSpan.Zero)
            {
                const int calmDownMinMilliSeconds = 300000;
                const int calmDownMaxMilliSeconds = 500000;
                Random random = new();
                int milliseconds = random.Next(calmDownMinMilliSeconds, calmDownMaxMilliSeconds);
                this.logger.LogDebug("{0} is going to sleep for {1} millisecond(s)", user.name, milliseconds);
                await Task.Delay(milliseconds, token);
            }
        }
    }
}
