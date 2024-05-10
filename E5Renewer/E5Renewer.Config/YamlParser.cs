using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using E5Renewer.Config;

namespace E5Renewer.Modules.ConfigParsers
{
    /// <summary>Yaml config parser.</summary>
    [Module]
    public class YamlParser : IConfigParser
    {
        /// <inheritdoc/>
        public string name { get => "YamlParser"; }
        /// <inheritdoc/>
        public string author { get => "E5Renewer"; }
        /// <inheritdoc/>
        public SemVer apiVersion { get => new(0, 1, 0); }
        /// <inheritdoc/>
        public bool IsSupported(string path) => path.EndsWith(".yaml") || path.EndsWith(".yml");
        /// <inheritdoc/>
        public async Task<RuntimeConfig> ParseConfig(string path)
        {
            RuntimeConfig runtimeConfig;
            using (StreamReader stream = File.OpenText(path))
            {
                IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                runtimeConfig = deserializer.Deserialize<RuntimeConfig>(await stream.ReadToEndAsync());
            }
            return runtimeConfig;
        }
    }
}
