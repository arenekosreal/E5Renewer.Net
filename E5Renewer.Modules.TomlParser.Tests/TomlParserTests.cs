namespace E5Renewer.Modules.TomlParser.Tests;

/// <summary>Test
/// <see cref="TomlParser"/>
/// </summary>
[TestClass]
public class TomlParserTests
{
    private readonly TomlParser parser = new();

    /// <summary>Ensure name.</summary>
    [TestMethod]
    public void TestName() => Assert.AreEqual("TomlParser", this.parser.name);
    /// <summary>Ensure author.</summary>
    [TestMethod]
    public void TestAuthor() => Assert.AreEqual("E5Renewer", this.parser.author);

    /// <summary>Test
    /// <see cref="TomlParser.IsSupported(string)" />
    /// </summary>
    [TestMethod]
    [DataRow("test.toml", true)]
    [DataRow("test.yaml", false)]
    [DataRow("test.json", false)]
    public void TestIsSupported(string path, bool result) => Assert.AreEqual(result, this.parser.IsSupported(path));

    /// <summary>Test
    /// <see cref="TomlParser.ParseConfigAsync(string)" />
    /// </summary>
    [TestMethod]
    [DataRow("", false)]
    [DataRow("auth_token = \"test-token\"", true)]
    public async Task TestParseConfigAsyncAuthToken(string json, bool result)
    {
        string tmpPath = Path.GetTempFileName();
        await File.WriteAllTextAsync(tmpPath, json);
        E5Renewer.Models.Config.Config config = await this.parser.ParseConfigAsync(tmpPath);
        bool compareResult = config.authToken == "test-token";
        Assert.AreEqual(result, compareResult);
    }
}
