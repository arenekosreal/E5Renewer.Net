using E5Renewer.Models.Secrets;

using Microsoft.Graph;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

using Azure.Core;
using Azure.Identity;

namespace E5Renewer.Models.GraphAPIs;

/// <summary><see cref="IUserClientProvider"/> implementation for providing user client in memory.</summary>
public class SimpleUserClientProvider : IUserClientProvider
{
    private readonly Dictionary<User, GraphServiceClient> clients = new();

    private readonly ILogger<SimpleSecretProvider> logger;
    private readonly ISecretProvider secretProvider;

    /// <summary>Initialize <see cref="SimpleSecretProvider"/> with arguments given.</summary>
    /// <param name="logger">The logger to generate logs.</param>
    /// <param name="secretProvider">The <see cref="ISecretProvider"/> implementation.</param>
    /// <remarks>All arguments should be injected by Asp.Net Core.</remarks>
    public SimpleUserClientProvider(ILogger<SimpleSecretProvider> logger, ISecretProvider secretProvider) =>
        (this.logger, this.secretProvider) = (logger, secretProvider);

    /// <inheritdoc/>
    public async Task<GraphServiceClient> GetClientForUserAsync(User user)
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
        return this.clients[user];
    }
}
