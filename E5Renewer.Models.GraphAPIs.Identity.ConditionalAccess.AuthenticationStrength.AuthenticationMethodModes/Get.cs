using E5Renewer.Models.Modules;

using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs.Identity.ConditionalAccess.AuthenticationStrength.AuthenticationMethodModes;
/// <summary>Identity.ConditionalAccess.AuthenticationStrength.AuthenticationMethodModes.Get api implementation.</summary>
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
    public string id { get => "Identity.ConditionalAccess.AuthenticationStrength.AuthenticationMethodModes.Get"; }

    /// <inheritdoc/>
    public async Task<object?> CallAsync(GraphServiceClient client) => await client.Identity.ConditionalAccess.AuthenticationStrength.AuthenticationMethodModes.GetAsync();
}
