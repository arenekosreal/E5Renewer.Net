using E5Renewer.Models.Statistics;

namespace E5Renewer.Tests.Models.Statistics;

/// <summary>Test
/// <see cref="UnixTimestampGenerator"/>
/// </summary>
[TestClass]
public class UnixTimestampGeneratorTests
{
    private readonly UnixTimestampGenerator generator;

    /// <summary>Initialize <see cref="UnixTimestampGeneratorTests"/> with no argument.</summary>
    public UnixTimestampGeneratorTests()
    {
        this.generator = new();
    }

    /// <summary>Test
    /// <see cref="UnixTimestampGenerator.GetUnixTimestamp"/>
    /// </summary>
    [TestMethod]
    public void TestGetUnixTimestamp()
    {
        long result1 = this.generator.GetUnixTimestamp();
        long result2 = this.generator.GetUnixTimestamp();
        Assert.AreNotSame(result1, result2);
    }
}
