using System.Security.Cryptography;

using E5Renewer.Models.Config;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace E5Renewer.Tests.Models.Config;

/// <summary>Test
/// <see cref="ConfigCertificatePasswordProvider" />
/// </summary>
[TestClass]
public class ConfigCertificatePasswordProviderTests
{
    private readonly string tmpFilePath = Path.GetTempFileName();
    private readonly ConfigCertificatePasswordProvider provider;

    /// <summary>Initialize <see cref="ConfigCertificatePasswordProviderTests"/> with no argument.</summary>
    public ConfigCertificatePasswordProviderTests()
    {
        Random random = new();
        byte[] buffer = new byte[1024];
        random.NextBytes(buffer);
        byte[] hash = SHA512.HashData(buffer);
        string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
        Dictionary<string, string?> passwords = new()
        {
            {hashString, "example-secret"}
        };
        using (Stream stream = File.OpenWrite(this.tmpFilePath))
        {
            stream.Write(buffer);
        }
        ILogger<ConfigCertificatePasswordProvider> logger = Substitute.For<ILogger<ConfigCertificatePasswordProvider>>();
        this.provider = new(logger, passwords);
    }
    /// <summary>Test
    /// <see cref="ConfigCertificatePasswordProvider.GetPasswordForCertificateAsync(string)" />
    /// </summary>
    [TestMethod]
    public async Task TestGetPasswordForCertificateAsync()
    {
        string? password = await this.provider.GetPasswordForCertificateAsync(this.tmpFilePath);
        Assert.AreEqual("example-secret", password);
    }
}
