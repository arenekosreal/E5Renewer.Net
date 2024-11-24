using E5Renewer.Models.Modules;

using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs.Policies.CrossTenantAccessPolicy.Templates.MultiTenantOrganizationPartnerConfiguration;
/// <summary>Policies.CrossTenantAccessPolicy.Templates.MultiTenantOrganizationPartnerConfiguration.Get api implementation.</summary>
[Module]
public class Get : BasicModule, IAPIFunction
{
    /// <inheritdoc/>
    public ILogger logger { get; }

    /// <summary>Initialize <see cref="Get"/> class.</summary>
    /// <param name="logger">The <see cref="ILogger{IAPIFunction}"/> implementation.</param>
    /// <remarks>All params should be injected by Asp.Net Core.</remarks>
    public Get(ILogger<Get> logger) => this.logger = logger;

    /// <inheritdoc/>
    public string id { get => "Policies.CrossTenantAccessPolicy.Templates.MultiTenantOrganizationPartnerConfiguration.Get"; }

    /// <inheritdoc/>
    public async Task<object?> CallAsync(GraphServiceClient client) => await client.Policies.CrossTenantAccessPolicy.Templates.MultiTenantOrganizationPartnerConfiguration.GetAsync();
}
