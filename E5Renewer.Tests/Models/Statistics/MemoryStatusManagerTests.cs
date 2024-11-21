using E5Renewer.Models.Statistics;

namespace E5Renewer.Tests.Models.Statistics;

/// <summary>Test
/// <see cref="MemoryStatusManager"/>
/// </summary>
[TestClass]
public class MemoryStatusManagerTests
{
    private readonly MemoryStatusManager manager;

    /// <summary>Initialize <see cref="MemoryStatusManagerTests"/> with no argument.</summary>
    public MemoryStatusManagerTests()
    {
        this.manager = new();
    }

    /// <summary>Test
    /// <see cref="MemoryStatusManager.GetRunningUsersAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGetRunningUsersAsync()
    {
        IEnumerable<string> runningUsers = await this.manager.GetRunningUsersAsync();
        int count = runningUsers.Count();
        Assert.AreEqual(0, count);
    }

    /// <summary>Test
    /// <see cref="MemoryStatusManager.GetWaitingUsersAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGetWaitingUsersAsync()
    {
        IEnumerable<string> waitingUsers = await this.manager.GetWaitingUsersAsync();
        int count = waitingUsers.Count();
        Assert.AreEqual(0, count);
    }

    /// <summary>Test
    /// <see cref="MemoryStatusManager.SetUserStatusAsync"/>
    /// </summary>
    [TestMethod]
    [DataRow("test-running", true, 1, 0)]
    [DataRow("test-waiting", false, 0, 1)]
    public async Task TestSetUserStatusAsync(string name, bool running, int targetRunningCount, int targetWaitingCount)
    {
        await this.manager.SetUserStatusAsync(name, running);
        int runningCount = (await this.manager.GetRunningUsersAsync()).Count();
        int waitingCount = (await this.manager.GetWaitingUsersAsync()).Count();
        Assert.AreEqual(targetRunningCount, runningCount);
        Assert.AreEqual(targetWaitingCount, waitingCount);
    }

    /// <summary>Test
    /// <see cref="MemoryStatusManager.GetResultsAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestGetResultsAsync()
    {
        IEnumerable<string> results = await this.manager.GetResultsAsync("test", "Test.Example");
        int count = results.Count();
        Assert.AreEqual(0, count);
    }

    /// <summary>Test
    /// <see cref="MemoryStatusManager.SetResultAsync"/>
    /// </summary>
    [TestMethod]
    public async Task TestSetResultAsync()
    {
        await this.manager.SetResultAsync("test", "Test.Example.Set", "Success");
    }
}
