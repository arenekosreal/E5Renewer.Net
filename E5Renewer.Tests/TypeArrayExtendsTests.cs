namespace E5Renewer.Tests;

/// <summary>
/// Test
/// <see cref="TypeArrayExtends"/>
/// </summary>
[TestClass]
public class TypeArrayExtendsTests
{
    private void TestGetNonAbstractClassesAssainableToHelper<T>(uint count)
    {
        IEnumerable<Type> typesFound = typeof(TypeArrayExtendsTests).Assembly.GetTypes().GetNonAbstractClassesAssainableTo<T>();
        Assert.AreEqual((int)count, typesFound.Count());
    }

    /// <summary>
    /// Test
    /// <see cref="TestGetNonAbstractClassesAssainableTo"/>
    /// </summary>
    [TestMethod]
    public void TestGetNonAbstractClassesAssainableTo()
    {
        this.TestGetNonAbstractClassesAssainableToHelper<TypeArrayExtendsTests>(1);
    }
}
