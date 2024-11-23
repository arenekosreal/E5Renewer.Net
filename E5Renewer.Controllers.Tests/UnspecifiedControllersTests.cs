using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using NSubstitute;

namespace E5Renewer.Controllers.Tests;

/// <summary>Test
/// <see cref="UnspecifiedController"/>
/// </summary>
[TestClass]
public class UnspecifiedControllerTests
{
    private readonly UnspecifiedController controller;
    private readonly IJsonResponse response;

    /// <summary>Initialize <see cref="UnspecifiedControllerTests"/> with no argument.</summary>
    public UnspecifiedControllerTests()
    {
        ILogger<UnspecifiedController> logger = Substitute.For<ILogger<UnspecifiedController>>();
        IDummyResultGenerator dummyResultGenerator = Substitute.For<IDummyResultGenerator>();
        this.response = Substitute.For<IJsonResponse>();
        HttpContext context = new DefaultHttpContext();
        dummyResultGenerator.GenerateDummyResultAsync(context).Returns(Task.FromResult(this.response));
        dummyResultGenerator.GenerateDummyResult(context).Returns(this.response);
        this.controller = new(logger, dummyResultGenerator);
        this.controller.ControllerContext.HttpContext = context;
    }

    /// <summary>Test
    /// <see cref="UnspecifiedController.Handle"/>
    /// </summary>
    [TestMethod]
    public async Task TestHandle()
    {
        IJsonResponse result = await this.controller.Handle();
        Assert.AreEqual(this.response, result);
    }
}

