using System.Text.Json;
using E5Renewer.Config;
namespace E5Renewer.Modules.ConfigParsers
{
    [Module]
    public class JsonParser : IConfigParser
    {
        public string name { get => "JsonParser"; }
        public string author { get => "E5Renewer"; }
        public SemVer apiVersion { get => new(0, 1, 0); }
        public bool IsSupported(string path) => path.EndsWith(".json");

        public async Task<RuntimeConfig> ParseConfig(string path)
        {
            RuntimeConfig? runtimeConfig;
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };
            using (FileStream stream = File.OpenRead(path))
            {
                runtimeConfig = await JsonSerializer.DeserializeAsync<RuntimeConfig>(stream, options);
            }
            return runtimeConfig ?? new();
        }

    }
}
