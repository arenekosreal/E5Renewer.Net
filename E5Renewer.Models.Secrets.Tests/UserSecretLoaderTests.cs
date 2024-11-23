namespace E5Renewer.Models.Secrets.Tests;

/// <summary>Base class to provide some utils for testing
/// <see cref="IUserSecretLoader"/> implementations.
/// </summary>
public abstract class UserSecretLoaderTests
{
    /// <summary>Generate a temp file with content given.</summary>
    /// <returns>The <see cref="FileInfo"/> of temp file.</returns>
    protected async Task<FileInfo> PrepareContent(string content)
    {
        FileInfo tempFile = new(Path.GetTempFileName());
        using (StreamWriter streamWriter = tempFile.CreateText())
        {
            await streamWriter.WriteAsync(content);
            await streamWriter.FlushAsync();
        }
        return tempFile;
    }

    /// <summary>Test <see cref="IUserSecretLoader.IsSupported"/></summary>
    public abstract void TestIsSupported(string path, bool result);

    /// <summary>Test <see cref="IUserSecretLoader.LoadSecretAsync"/></summary>
    public abstract Task TestLoadSecretAsync(string jsonContent, bool expected);
}
