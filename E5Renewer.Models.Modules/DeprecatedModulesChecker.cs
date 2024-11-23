using Microsoft.Extensions.Logging;

namespace E5Renewer.Models.Modules;

/// <summary>Check if the module is deprecated.</summary>
[Module]
public class DeprecatedModulesChecker : BasicModule, IModulesChecker
{
    private readonly ILogger<DeprecatedModulesChecker> logger;

    /// <summary>Initialize the <c>DeprecatedModulesChecker</c> instance.</summary>
    /// <param name="logger">The logger factory to create logger to generate outputs.</param>
    /// <remarks>All the parameters should be injected by Asp.Net Core.</remarks>
    public DeprecatedModulesChecker(ILogger<DeprecatedModulesChecker> logger) =>
        this.logger = logger;

    /// <inheritdoc/>
    public void CheckModules(IModule module)
    {
        if (module.isDeprecated)
        {
            this.logger.LogWarning("You are using a deprecated module {0} by {1}!", module.name, module.author);
        }
    }
}

