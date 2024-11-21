using E5Renewer.Models.Secrets;
using E5Renewer.Models.Secrets.Json;

namespace E5Renewer.Tests.Models.Secrets.Json;

/// <summary>Test
/// <see cref="JsonUserSecretLoader"/>
/// </summary>
[TestClass]
public class JsonUserSecretLoaderTests : UserSecretLoaderTests
{
    private const string validJsonContentWithSecret = """{"users":[{"name":"test", "tenant_id":"test","client_id":"test","secret":"test"}]}""";
    private const string validJsonContentWithSecretAndDays = """{"users":[{"name":"test", "tenant_id":"test","client_id":"test","secret":"test", "days": [1]}]}""";

    private const string invalidJsonContentWithCertificateNotExist = """{"users":[{"name":"test", "tenant_id":"test","client_id":"test","certificate":"not-exist"}]}""";
    private const string invalidJsonContentWithoutSecretOrCertificate = """{"users":[{"name":"test", "tenant_id":"test","client_id":"test"}]}""";

    private readonly JsonUserSecretLoader loader;

    /// <summary>Initialize <see cref="JsonUserSecretLoaderTests"/> with no argument.</summary>
    public JsonUserSecretLoaderTests()
    {
        this.loader = new();
    }

    /// <summary>Test
    /// <see cref="JsonUserSecretLoader.IsSupported"/>
    /// </summary>
    [TestMethod]
    [DataRow("/path/to/json", false)]
    [DataRow("C:\\json", false)]
    [DataRow("/path/to/file.json", true)]
    [DataRow("C:\\file.json", true)]
    public override void TestIsSupported(string path, bool result)
    {
        FileInfo info = new(path);
        bool actual = this.loader.IsSupported(info);
        Assert.AreEqual(result, actual);
    }

    /// <summary>Test
    /// <see cref="JsonUserSecretLoader.LoadSecretAsync"/>
    /// </summary>
    [TestMethod]
    [DataRow(validJsonContentWithSecret, true)]
    [DataRow(validJsonContentWithSecretAndDays, true)]
    [DataRow(invalidJsonContentWithoutSecretOrCertificate, false)]
    [DataRow(invalidJsonContentWithCertificateNotExist, false)]
    public override async Task TestLoadSecretAsync(string jsonContent, bool expected)
    {
        FileInfo tempFile = await this.PrepareContent(jsonContent);
        UserSecret secret = await this.loader.LoadSecretAsync(tempFile);
        Assert.AreEqual(expected, secret.valid);
    }
}
