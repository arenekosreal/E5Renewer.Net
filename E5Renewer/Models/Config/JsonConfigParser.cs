using System.Text.Json;

using E5Renewer.Models.Modules;

namespace E5Renewer.Models.Config
{
    /// <summary>class for parsing json config.</summary>
    [Module]
    public class JsonConfigParser : IConfigParser
    {
        /// <inheritdoc/>
        public string name { get => nameof(JsonConfigParser); }

        /// <inheritdoc/>
        public string author { get => "E5Renewer"; }

        /// <inheritdoc/>
        public SemanticVersioning.Version apiVersion
        {
            get => typeof(JsonConfigParser).Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
        }

        /// <inheritdoc/>
        public bool IsSupported(string path) => path.EndsWith(".json");

        /// <inheritdoc/>
        public async ValueTask<Config> ParseConfigAsync(string path)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };
            using (FileStream fileStream = File.OpenRead(path))
            {
                return await JsonSerializer.DeserializeAsync<Config>(fileStream, options) ?? new();
            }
        }
    }
}
