using System.Text.Json;

using E5Renewer.Models.Modules;

namespace E5Renewer.Models.Secrets.Json;

/// <summary>Class for loading user secret from json file.</summary>
[Module]
public class JsonUserSecretLoader : BasicModule, IUserSecretLoader
{
    private readonly Dictionary<FileInfo, UserSecret> cache = new();

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

