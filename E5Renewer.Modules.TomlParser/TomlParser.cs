using CaseConverter;

using E5Renewer.Models;
using E5Renewer.Models.Config;
using E5Renewer.Models.Modules;

using Tomlyn;

namespace E5Renewer.Modules.TomlParser;

/// <summary>
/// Parse toml to
/// <see cref="Config">Config</see>.
/// </summary>
[Module]
public class TomlParser : IConfigParser
{
    /// <inheritdoc/>
    public string name { get => nameof(TomlParser); }

    /// <inheritdoc/>
    public string author { get => "E5Renewer"; }

    /// <inheritdoc/>
    public SemanticVersioning.Version apiVersion
    {
        get => typeof(TomlParser).Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
    }

    /// <inheritdoc/>
    public bool IsSupported(string path) => path.EndsWith(".toml");

    /// <inheritdoc/>
    public async Task<Config> ParseConfigAsync(string path)
    {
        Config runtimeConfig;
        using (StreamReader stream = File.OpenText(path))
        {
            runtimeConfig = Toml.ToModel<Config>(
                await stream.ReadToEndAsync(), path, new()
                {
                    ConvertPropertyName = (input) => input.SnakeCaseToCamelCase()
                }
            );
        }
        return runtimeConfig;
    }
}
