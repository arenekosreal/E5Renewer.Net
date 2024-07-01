using E5Renewer.Models;
using E5Renewer.Models.Config;
using E5Renewer.Models.Modules;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace E5Renewer.Modules.YamlParser;

/// <summary>
/// Parse yaml to
/// <see cref="Config">Config</see>.
/// </summary>
[Module]
public class YamlParser : IConfigParser
{
    /// <inheritdoc/>
    public string name { get => nameof(YamlParser); }

    /// <inheritdoc/>
    public string author { get => "E5Renewer"; }

    /// <inheritdoc/>
    public SemanticVersioning.Version apiVersion
    {
        get => typeof(YamlParser).Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
    }

    /// <inheritdoc/>
    public bool IsSupported(string path) => path.EndsWith(".yaml") || path.EndsWith(".yml");

    /// <inheritdoc/>
    public async Task<Config> ParseConfigAsync(string path)
    {
        Config runtimeConfig;
        using (StreamReader stream = File.OpenText(path))
        {
            IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            runtimeConfig = deserializer.Deserialize<Config>(await stream.ReadToEndAsync());
        }
        return runtimeConfig;
    }

}
