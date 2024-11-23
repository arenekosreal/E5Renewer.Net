namespace E5Renewer.Models.Statistics;

/// <summary>
/// The api interface to manage user status and
/// <see cref="APICallResult">APICallResult</see>.
/// </summary>
public interface IStatusManager
{
    /// <summary>Get all running users' names.</summary>
    public Task<IEnumerable<string>> GetRunningUsersAsync();

    /// <summary>Get all waiting users' names.</summary>
    public Task<IEnumerable<string>> GetWaitingUsersAsync();

    /// <summary>Set user status by user name.</summary>
    public Task SetUserStatusAsync(string name, bool running);

    /// <summary>Get all results by user name and api id.</summary>
    public Task<IEnumerable<string>> GetResultsAsync(string name, string id);

    /// <summary>Update result by user name, api id and result string.</summary>
    public Task SetResultAsync(string name, string id, string result);
}

