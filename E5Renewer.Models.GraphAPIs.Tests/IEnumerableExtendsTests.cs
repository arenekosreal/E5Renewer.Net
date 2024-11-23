namespace E5Renewer.Models.GraphAPIs.Tests;

/// <summary>Test
/// <see cref="IEnumerableExtends" />
/// </summary>
[TestClass]
public class TestIEnumerableExtends
{
    /// <summary>Test
    /// <see cref="IEnumerableExtends.GetDifferentItemsByWeight{T}(IEnumerable{T}, Func{T, int}, int)" />
    /// </summary>
    [TestMethod]
    public void TestGetDifferentItemsByWeight()
    {
        string[] inputs = ["testA", "testB", "testC"];
        string output = inputs.GetDifferentItemsByWeight((i) => 1, 1).First();
        Assert.IsTrue(inputs.Contains(output));
    }
}
