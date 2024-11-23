namespace E5Renewer.Models.Modules;

/// <summary> Basic module which filled name, author and apiVersion information of <see cref="IModule"/></summary>
/// <remarks> All information filled here are ours, so most of the time you should **NOT** use this.</remarks>
public class BasicModule : IModule
{
    /// <inheritdoc/>
    public string name { get => this.GetType().Name; }

    /// <inheritdoc/>
    public string author { get => "E5Renewer"; }

    /// <inheritdoc/>
    public SemanticVersioning.Version apiVersion
    {
        get => this.GetType().Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
    }
}


