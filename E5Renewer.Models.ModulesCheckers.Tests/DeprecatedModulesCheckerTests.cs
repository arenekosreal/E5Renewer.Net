using E5Renewer.Models.Modules;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace E5Renewer.Models.ModulesCheckers.Tests;

/// <summary>Test
/// <see cref="DeprecatedModulesChecker"/>
/// </summary>
[TestClass]
public class DeprecatedModulesCheckerTests
{
    private readonly DeprecatedModulesChecker checker;

    /// <summary>Initialize <see cref="DeprecatedModulesCheckerTests"/> with no argument.</summary>
    public DeprecatedModulesCheckerTests()
    {
        ILogger<DeprecatedModulesChecker> logger = Substitute.For<ILogger<DeprecatedModulesChecker>>();
        this.checker = new(logger);
    }

    /// <summary>Test
    /// <see cref="DeprecatedModulesChecker.CheckModules"/>
    /// </summary>
    [TestMethod]
    public void TestCheckModules()
    {
        IModule module = Substitute.For<IModule>();
        module.isDeprecated.Returns(false);
        this.checker.CheckModules(module);
    }
}
