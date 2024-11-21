using System.Text.Json;

using E5Renewer.Models.Modules;

namespace E5Renewer.Models.Secrets.Json
{
    /// <summary>Class for loading user secret from json file.</summary>
    [Module]
    public class JsonUserSecretLoader : IUserSecretLoader
    {
        private readonly Dictionary<FileInfo, UserSecret> cache = new();

        /// <inheritdoc/>
        public string name { get => nameof(JsonUserSecretLoader); }

        /// <inheritdoc/>
        public string author { get => "E5Renewer"; }

        /// <inheritdoc/>
        public SemanticVersioning.Version apiVersion
        {
            get => typeof(JsonUserSecretLoader).Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
        }

        /// <inheritdoc/>
        public bool IsSupported(FileInfo userSecret)
        {
            return userSecret.Name.EndsWith(".json");
        }

        /// <inheritdoc/>
        public async Task<UserSecret> LoadSecretAsync(FileInfo userSecret)
        {
            if (!this.cache.ContainsKey(userSecret))
            {
                using (FileStream fileStream = userSecret.OpenRead())
                {
                    JsonSerializableUserSecret? secret = await JsonSerializer.DeserializeAsync<JsonSerializableUserSecret>(
                        fileStream,
                        LoaderJsonSerializerContext.Default.JsonSerializableUserSecret
                    );
                    this.cache[userSecret] = secret?.value ?? new();
                }
            }
            return this.cache[userSecret];

        }
    }
}
