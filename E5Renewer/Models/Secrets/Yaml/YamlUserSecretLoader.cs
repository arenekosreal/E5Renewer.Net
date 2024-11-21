using E5Renewer.Models.Modules;

using VYaml.Serialization;

namespace E5Renewer.Models.Secrets.Yaml
{
    /// <summary>Class for loading user secret from yaml file.</summary>
    [Module]
    public class YamlUserSecretLoader : IUserSecretLoader
    {
        /// <inheritdoc/>
        public string name { get => nameof(YamlUserSecretLoader); }

        /// <inheritdoc/>
        public string author { get => "E5Renewer"; }

        /// <inheritdoc/>
        public SemanticVersioning.Version apiVersion
        {
            get => typeof(YamlUserSecretLoader).Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
        }

        /// <inheritdoc/>
        public bool IsSupported(FileInfo userSecret) => userSecret.Name.EndsWith(".yml") || userSecret.Name.EndsWith(".yaml");

        /// <inheritdoc/>
        public async Task<UserSecret> LoadSecretAsync(FileInfo userSecret)
        {
            YamlSerializeCompatibleUserSecret userSecretObject;
            using (FileStream fileStream = userSecret.OpenRead())
            {
                userSecretObject = await YamlSerializer.DeserializeAsync<YamlSerializeCompatibleUserSecret>(fileStream);
            }
            return userSecretObject.value;
        }

    }
}
