using System.Text.Json;
using E5Renewer.Config;
namespace E5Renewer.Modules.ConfigParsers
{
    /// <summary> Json config parser.</summary>
    [Module]
    public class JsonParser : IConfigParser
    {
        /// <inheritdoc/>
        public string name { get => "JsonParser"; }
        /// <inheritdoc/>
        public string author { get => "E5Renewer"; }
        /// <inheritdoc/>
        public SemVer apiVersion { get => new(0, 1, 0); }
        /// <inheritdoc/>
        public bool IsSupported(string path) => path.EndsWith(".json");

        /// <inheritdoc/>
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
