using Microsoft.Graph;
using Azure.Identity;
using System.Reflection;
using E5Renewer.Processor;
using E5Renewer.Config;

namespace E5Renewer.Modules.APICallers
{
    /// <summary>A APICaller implementation which calls msgraph apis randomly.</summary>
    [Module]
    public class RandomAPICaller : IAPICaller
    {
        private readonly ILogger<RandomAPICaller> logger = LoggerFactory.Create(
            (build) => build.AddSimpleConsole(
                (options) =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = E5Renewer.Constraints.loggingTimeFormat;
                }
            ).SetMinimumLevel(E5Renewer.Constraints.loggingLevel)
        ).CreateLogger<RandomAPICaller>();
        private static readonly Dictionary<string, APIFunction> apiFunctions = APIHelper.GetAPIFunctions();
        private readonly Dictionary<GraphUser, GraphServiceClient> userClientsMap = new();
        /// <inheritdoc/>
        public string name { get => "RandomAPICaller"; }
        /// <inheritdoc/>
        public string author { get => "E5Renewer"; }
        /// <inheritdoc/>
        public SemVer apiVersion { get => new(0, 1, 0); }
        /// <inheritdoc/>
        public async Task CallNextAPI(bool update = true)
        {
            List<Task<APICallResult>> tasks = new();
            Random random = new();
            foreach (KeyValuePair<GraphUser, GraphServiceClient> kv in userClientsMap)
            {
                int i = random.Next(apiFunctions.Count());
                string apiName = apiFunctions.Keys.ElementAt(i);
                APIFunction apiFunc = apiFunctions.Values.ElementAt(i);
                Func<GraphUser, GraphServiceClient, Task<APICallResult>> invoke = async delegate (GraphUser user, GraphServiceClient client)
                {
                    logger.LogDebug("Calling api {0}", apiName);
                    APICallResult result = await apiFunc(client);
                    if (update)
                    {
                        user.UpdateResult(result, apiName);
                    }
                    return result;

                };
                tasks.Add(invoke(kv.Key, kv.Value));
            }
            await Task.WhenAll(tasks.ToArray());
        }
        /// <inheritdoc/>
        public void AddUser(GraphUser user)
        {
            if (!userClientsMap.ContainsKey(user))
            {
                ClientCertificateCredentialOptions options = new()
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };
                ClientSecretCredential credential = new(
                    user.tenantId, user.clientId, user.secret,
                    options
                );
                GraphServiceClient client = new(
                    credential, new[] { "https://graph.microsoft.com/.default" }
                );
                userClientsMap[user] = client;
            }
        }
    }
}
