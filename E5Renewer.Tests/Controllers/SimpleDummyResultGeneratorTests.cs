using E5Renewer.Controllers;
using E5Renewer.Models.Statistics;

using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace E5Renewer.Tests.Controllers;

/// <summary>Test
/// <see cref="SimpleDummyResultGenerator"/>
/// </summary>
[TestClass]
public class SimpleDummyResultGeneratorTests
{
    private readonly SimpleDummyResultGenerator dummyResultGenerator;

    /// <summary>Initialize <see cref="SimpleDummyResultGeneratorTests"/> with no argument.</summary>
    public SimpleDummyResultGeneratorTests()
    {
        ILogger<SimpleDummyResultGenerator> logger = Substitute.For<ILogger<SimpleDummyResultGenerator>>();
        IUnixTimestampGenerator timestampGenerator = Substitute.For<IUnixTimestampGenerator>();
        timestampGenerator.GetUnixTimestamp().ReturnsForAnyArgs((long)42);
        this.dummyResultGenerator = new(logger, timestampGenerator);
    }

    /// <summary>Test
    /// <see cref="SimpleDummyResultGenerator.GenerateDummyResultAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGenerateDummyResultAsync()
    {
        HttpContext context = new DefaultHttpContext();
        InvokeResult result = await this.dummyResultGenerator.GenerateDummyResultAsync(context);
        Assert.AreEqual((long)42, result.timestamp);
    }
    /// <summary>Test
    /// <see cref="SimpleDummyResultGenerator.GenerateDummyResult"/>
    /// </summary>
    [TestMethod]
    public void TestGenerateDummyResult()
    {
        HttpContext context = new DefaultHttpContext();
        InvokeResult result = this.dummyResultGenerator.GenerateDummyResult(context);
        Assert.AreEqual((long)42, result.timestamp);
    }
}
