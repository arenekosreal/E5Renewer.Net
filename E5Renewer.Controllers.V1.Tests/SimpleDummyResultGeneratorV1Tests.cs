using E5Renewer.Models.Statistics;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using NSubstitute;

namespace E5Renewer.Controllers.V1.Tests;

/// <summary>Test
/// <see cref="SimpleDummyResultGeneratorV1"/>
/// </summary>
[TestClass]
public class SimpleDummyResultGeneratorTests
{
    private readonly SimpleDummyResultGeneratorV1 dummyResultGenerator;

    /// <summary>Initialize <see cref="SimpleDummyResultGeneratorTests"/> with no argument.</summary>
    public SimpleDummyResultGeneratorTests()
    {
        ILogger<SimpleDummyResultGeneratorV1> logger = Substitute.For<ILogger<SimpleDummyResultGeneratorV1>>();
        IUnixTimestampGenerator timestampGenerator = Substitute.For<IUnixTimestampGenerator>();
        timestampGenerator.GetUnixTimestamp().ReturnsForAnyArgs((long)42);
        this.dummyResultGenerator = new(logger, timestampGenerator);
    }

    /// <summary>Test
    /// <see cref="SimpleDummyResultGeneratorV1.GenerateDummyResultAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGenerateDummyResultAsync()
    {
        HttpContext context = new DefaultHttpContext();
        JsonAPIV1Response result = (JsonAPIV1Response)await this.dummyResultGenerator.GenerateDummyResultAsync(context);
        Assert.AreEqual((long)42, result.timestamp);
    }
    /// <summary>Test
    /// <see cref="SimpleDummyResultGeneratorV1.GenerateDummyResult"/>
    /// </summary>
    [TestMethod]
    public void TestGenerateDummyResult()
    {
        HttpContext context = new DefaultHttpContext();
        JsonAPIV1Response result = (JsonAPIV1Response)this.dummyResultGenerator.GenerateDummyResult(context);
        Assert.AreEqual((long)42, result.timestamp);
    }
}
