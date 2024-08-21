namespace E5Renewer.Modules.YamlParser.Tests;

/// <summary>Test
/// <see cref="YamlParser" />
/// </summary>
[TestClass]
public class YamlParserTests
{
    private readonly YamlParser parser = new();

    /// <summary>Ensure name.</summary>
    [TestMethod]
    public void TestName() => Assert.AreEqual("YamlParser", this.parser.name);

    /// <summary>Ensure author.</summary>
    [TestMethod]
    public void TestAuthor() => Assert.AreEqual("E5Renewer", this.parser.author);

    /// <summary>Test
    /// <see cref="YamlParser.IsSupported(string)" />
    /// </summary>
    [TestMethod]
    [DataRow("test.toml", false)]
    [DataRow("test.yaml", true)]
    [DataRow("test.yml", true)]
    [DataRow("test.json", false)]
    public void TestIsSupported(string path, bool result) => Assert.AreEqual(result, this.parser.IsSupported(path));

    /// <summary>Test
    /// <see cref="YamlParser.ParseConfigAsync(string)" />
    /// </summary>
    [TestMethod]
    [DataRow("{}", false)]
    [DataRow("auth_token: test-token", true)]
    public async Task TestParseConfigAsyncAuthToken(string json, bool result)
    {
        string tmpPath = Path.GetTempFileName();
        await File.WriteAllTextAsync(tmpPath, json);
        E5Renewer.Models.Config.Config config = await this.parser.ParseConfigAsync(tmpPath);
        bool compareResult = config.authToken == "test-token";
        Assert.AreEqual(result, compareResult);
    }
}
