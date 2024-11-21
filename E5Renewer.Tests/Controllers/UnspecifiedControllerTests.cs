using E5Renewer.Controllers;

using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace E5Renewer.Tests.Controllers;

/// <summary>Test
/// <see cref="UnspecifiedController"/>
/// </summary>
[TestClass]
public class UnspecifiedControllerTests
{
    private readonly UnspecifiedController controller;

    /// <summary>Initialize <see cref="UnspecifiedControllerTests"/> with no argument.</summary>
    public UnspecifiedControllerTests()
    {
        ILogger<UnspecifiedController> logger = Substitute.For<ILogger<UnspecifiedController>>();
        IDummyResultGenerator dummyResultGenerator = Substitute.For<IDummyResultGenerator>();
        InvokeResult result = new();
        HttpContext context = new DefaultHttpContext();
        dummyResultGenerator.GenerateDummyResultAsync(context).Returns(Task.FromResult(result));
        dummyResultGenerator.GenerateDummyResult(context).Returns(result);
        this.controller = new(logger, dummyResultGenerator);
        this.controller.ControllerContext.HttpContext = context;
    }

    /// <summary>Test
    /// <see cref="UnspecifiedController.Handle"/>
    /// </summary>
    [TestMethod]
    public async Task TestHandle()
    {
        InvokeResult result = await this.controller.Handle();
        Assert.AreEqual(new(), result);
    }
}

