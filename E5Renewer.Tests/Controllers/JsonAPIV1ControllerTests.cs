using E5Renewer.Controllers;
using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Statistics;

using Microsoft.Extensions.Logging;

using NSubstitute;
using Microsoft.AspNetCore.Http;

namespace E5Renewer.Tests.Controllers;

/// <summary>Test
/// <see cref="JsonAPIV1Controller"/>
/// </summary>
[TestClass]
public class JsonAPIV1ControllerTests
{
    private readonly JsonAPIV1Controller controller;

    /// <summary>Initialize <see cref="JsonAPIV1ControllerTests"/> with no argument.</summary>
    public JsonAPIV1ControllerTests()
    {
        ILogger<JsonAPIV1Controller> logger = Substitute.For<ILogger<JsonAPIV1Controller>>();

        IStatusManager statusManager = Substitute.For<IStatusManager>();
        Task<IEnumerable<string>> runningResult = Task.FromResult<IEnumerable<string>>(["testRunning"]);
        Task<IEnumerable<string>> waitingResult = Task.FromResult<IEnumerable<string>>(["testWaiting"]);
        statusManager.GetRunningUsersAsync().Returns(runningResult);
        statusManager.GetWaitingUsersAsync().Returns(waitingResult);
        statusManager.GetResultsAsync("testName", "testId").ReturnsForAnyArgs(Task.FromResult<IEnumerable<string>>(["200 - OK"]));

        IAPIFunction apiFunction = Substitute.For<IAPIFunction>();
        apiFunction.id.Returns("test");
        List<IAPIFunction> apiFunctions = [apiFunction];

        IUnixTimestampGenerator generator = Substitute.For<IUnixTimestampGenerator>();
        generator.GetUnixTimestamp().Returns((long)42);

        IDummyResultGenerator dummyResultGenerator = Substitute.For<IDummyResultGenerator>();
        InvokeResult dummyResult = new();
        HttpContext context = new DefaultHttpContext();
        dummyResultGenerator.GenerateDummyResultAsync(context).ReturnsForAnyArgs(dummyResult);
        dummyResultGenerator.GenerateDummyResult(context).ReturnsForAnyArgs(dummyResult);

        this.controller = new(logger, statusManager, apiFunctions, generator, dummyResultGenerator);
    }
    /// <summary>Test
    /// <see cref="JsonAPIV1Controller.GetListApis" />
    /// </summary>
    [TestMethod]
    public async Task TestGetListApis()
    {
        InvokeResult result = await this.controller.GetListApis();
        Assert.AreEqual(0, result.args.Count);
        Assert.AreEqual("list_apis", result.method);
        string? id = ((IEnumerable<string>?)result.result)?.First();
        Assert.AreEqual("test", id);
    }
    /// <summary>Test
    /// <see cref="JsonAPIV1Controller.GetRunningUsers" />
    /// </summary>
    [TestMethod]
    public async Task TestGetRunningUsers()
    {
        InvokeResult result = await this.controller.GetRunningUsers();
        Assert.AreEqual(0, result.args.Count);
        Assert.AreEqual("running_users", result.method);
        string? user = ((IEnumerable<string>?)result.result)?.First();
        Assert.AreEqual("testRunning", user);
    }
    /// <summary>Test
    /// <see cref="JsonAPIV1Controller.GetWaitingUsers" />
    /// </summary>
    [TestMethod]
    public async Task TestGetWaitingUsers()
    {
        InvokeResult result = await this.controller.GetWaitingUsers();
        Assert.AreEqual(0, result.args.Count);
        Assert.AreEqual("waiting_users", result.method);
        string? user = ((IEnumerable<string>?)result.result)?.First();
        Assert.AreEqual("testWaiting", user);
    }
    /// <summary>Test
    /// <see cref="JsonAPIV1Controller.GetUserResults(string, string)"/>
    /// </summary>
    [TestMethod]
    public async Task TestGetUserResults()
    {
        InvokeResult result = await this.controller.GetUserResults("testName", "testId");
        Assert.AreEqual(2, result.args.Count);
        Assert.AreEqual("testName", result.args["user"]);
        Assert.AreEqual("testId", result.args["api_name"]);
        string? status = ((IEnumerable<string>?)result.result)?.First();
        Assert.AreEqual("200 - OK", status);
    }
    /// <summary>Test
    /// <see cfef="JsonAPIV1Controller.Handle" />
    /// </summary>
    [TestMethod]
    public async Task TestHandle()
    {
        InvokeResult result = await this.controller.Handle();
        Assert.AreEqual(new(), result);
    }
}
