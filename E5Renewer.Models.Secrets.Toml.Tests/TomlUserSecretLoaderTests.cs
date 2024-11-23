using E5Renewer.Models.Secrets.Tests;

namespace E5Renewer.Models.Secrets.Toml.Tests;

/// <summary>Test
/// <see cref="TomlUserSecretLoader"/>
/// </summary>
[TestClass]
public class TomlUserSecretLoaderTests : UserSecretLoaderTests
{
    private const string validTomlContentWithSecret =
        """
        [[users]]
        name="test"
        tenant_id="test"
        client_id="test"
        secret="test"
        """;
    private const string validTomlContentWithSecretAndDays =
        """
        [[users]]
        name="test"
        tenant_id="test"
        client_id="test"
        secret="test"
        days=[1]
        """;

    private const string invalidTomlContentWithCertificateNotExist =
        """
        [[users]]
        name="test"
        tenant_id="test"
        client_id="test"
        certificate="not-exist"
        """;
    private const string invalidTomlContentWithoutSecretOrCertificate =
        """
        [[users]]
        name="test"
        tenant_id="test"
        client_id="test"
        """;

    private readonly TomlUserSecretLoader loader;

    /// <summary>Initialize <see cref="TomlUserSecretLoaderTests"/> with no argument.</summary>
    public TomlUserSecretLoaderTests() => this.loader = new();

    /// <summary>Test
    /// <see cref="TomlUserSecretLoader.IsSupported"/>
    /// </summary>
    [TestMethod]
    [DataRow("/path/to/toml", false)]
    [DataRow("C:\\toml", false)]
    [DataRow("/path/to/file.toml", true)]
    [DataRow("C:\\file.toml", true)]
    public override void TestIsSupported(string path, bool result)
    {
        FileInfo info = new(path);
        bool actual = this.loader.IsSupported(info);
        Assert.AreEqual(result, actual);
    }

    /// <summary>Test
    /// <see cref="TomlUserSecretLoader.LoadSecretAsync"/>
    /// </summary>
    [TestMethod]
    [DataRow(validTomlContentWithSecret, true)]
    [DataRow(validTomlContentWithSecretAndDays, true)]
    [DataRow(invalidTomlContentWithoutSecretOrCertificate, false)]
    [DataRow(invalidTomlContentWithCertificateNotExist, false)]
    public override async Task TestLoadSecretAsync(string tomlContent, bool expected)
    {
        FileInfo tempFile = await this.PrepareContent(tomlContent);
        UserSecret secret = await this.loader.LoadSecretAsync(tempFile);
        Assert.AreEqual(expected, secret.valid);
    }
}
