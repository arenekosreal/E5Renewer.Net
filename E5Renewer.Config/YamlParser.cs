using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using E5Renewer.Config;

namespace E5Renewer.Modules.ConfigParsers
{
    [Module]
    public class YamlParser : IConfigParser
    {
        public string name { get => "YamlParser"; }
        public string author { get => "E5Renewer"; }
        public SemVer apiVersion { get => new(0, 1, 0); }
        public bool IsSupported(string path) => path.EndsWith(".yaml") || path.EndsWith(".yml");
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
