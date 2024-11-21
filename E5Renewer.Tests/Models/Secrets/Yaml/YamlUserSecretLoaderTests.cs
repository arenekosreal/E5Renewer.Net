using E5Renewer.Models.Secrets;
using E5Renewer.Models.Secrets.Yaml;

namespace E5Renewer.Tests.Models.Secrets.Yaml;

/// <summary>Test
/// <see cref="YamlUserSecretLoader"/>
/// </summary>
[TestClass]
public class YamlUserSecretLoaderTests : UserSecretLoaderTests
{
    private const string validYamlContentWithSecret =
        """
        users:
          - name: test
            tenant_id: test
            client_id: test
            secret: test
        """;
    private const string validYamlContentWithSecretAndDays =
        """
        users:
          - name: test
            tenant_id: test
            client_id: test
            secret: test
            days:
              - 1  
        """;

    private const string invalidYamlContentWithCertificateNotExist =
        """
        users:
          - name: test
            tenant_id: test
            client_id: test
            certificate: not-exist
        """;
    private const string invalidYamlContentWithNoSecretOrCertificate =
        """
        users:
          - name: test
            tenant_id: test
            client_id: test
        """;

    private readonly YamlUserSecretLoader loader;

    /// <summary>Initialize <see cref="YamlUserSecretLoaderTests"/> with no argument.</summary>
    public YamlUserSecretLoaderTests() => this.loader = new();

    /// <summary>Test
    /// <see cref="YamlUserSecretLoader.IsSupported"/>
    /// </summary>
       [TestMethod]
    [DataRow("/path/to/yaml", false)]
    [DataRow("/path/to/yml", false)]
    [DataRow("C:\\yaml", false)]
    [DataRow("C:\\yml", false)]
    [DataRow("/path/to/file.yaml", true)]
    [DataRow("/path/to/file.yml", true)]
    [DataRow("C:\\file.yaml", true)]
    [DataRow("C:\\file.yml", true)]
    public override void TestIsSupported(string path, bool result)
    {
        FileInfo info = new(path);
        bool actual = this.loader.IsSupported(info);
        Assert.AreEqual(result, actual);
    }

    /// <summary>Test
    /// <see cref="YamlUserSecretLoader.LoadSecretAsync"/>
    /// </summary>
    [TestMethod]
    [DataRow(validYamlContentWithSecret, true)]
    [DataRow(validYamlContentWithSecretAndDays, true)]
    [DataRow(invalidYamlContentWithCertificateNotExist, false)]
    [DataRow(invalidYamlContentWithNoSecretOrCertificate, false)]
    public override async Task TestLoadSecretAsync(string yamlContent, bool expected)
    {
        FileInfo tempFile = await this.PrepareContent(yamlContent);
        UserSecret secret = await this.loader.LoadSecretAsync(tempFile);
        Assert.AreEqual(expected, secret.valid);
    }
}
