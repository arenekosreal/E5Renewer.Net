namespace E5Renewer.Models.Secrets;

/// <summary>Token overrides random generated one.</summary>
public class TokenOverride
{
    /// <value>Override in plain text.</value>
    public readonly string? token;

    /// <value>Override in file content.</value>
    public readonly FileInfo? tokenFile;

    /// <summary>Initialize <see cref="TokenOverride"/> with arguments given.</summary>
    public TokenOverride(string? token, FileInfo? tokenFile)
    {
        this.token = token;
        this.tokenFile = tokenFile;
    }
}

