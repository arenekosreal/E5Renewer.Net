using CsToml;

using E5Renewer.Models.Modules;

namespace E5Renewer.Models.Secrets.Toml;

/// <summary>Class for loading user secret from toml file.</summary>
[Module]
public class TomlUserSecretLoader : BasicModule, IUserSecretLoader
{
    /// <inheritdoc/>
    public bool IsSupported(FileInfo userSecret) => userSecret.Name.EndsWith(".toml");

    /// <inheritdoc/>
    public async Task<UserSecret> LoadSecretAsync(FileInfo userSecret)
    {
        TomlDocument document;
        using (FileStream fileStream = userSecret.OpenRead())
        {
            byte[] buffer = new byte[userSecret.Length];
            int length = await fileStream.ReadAsync(buffer);
            document = CsTomlSerializer.Deserialize<TomlDocument>(buffer.Take(length).ToArray());
        }

        return document.RootNode.ToUserSecret();
    }
}

