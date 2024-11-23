namespace E5Renewer.Models.Statistics;

/// <summary>Manage msgraph api call result in memory.</summary>
public class MemoryStatusManager : IStatusManager
{
    private readonly List<string> runningUsers = new();
    private readonly List<string> waitingUsers = new();
    private readonly Dictionary<string, Dictionary<string, List<string>>> results = new();

    /// <inheritdoc/>
    public Task<IEnumerable<string>> GetRunningUsersAsync()
    {
        return Task.FromResult(this.runningUsers.AsEnumerable());
    }

    /// <inheritdoc/>
    public Task<IEnumerable<string>> GetWaitingUsersAsync()
    {
        return Task.FromResult(this.waitingUsers.AsEnumerable());
    }

    /// <inheritdoc/>
    public Task SetUserStatusAsync(string name, bool running)
    {
        if (running)
        {
            if (this.waitingUsers.Contains(name))
            {
                this.waitingUsers.Remove(name);
            }
            if (!this.runningUsers.Contains(name))
            {
                this.runningUsers.Add(name);
            }
        }
        else
        {
            if (this.runningUsers.Contains(name))
            {
                this.runningUsers.Remove(name);
            }
            if (!this.waitingUsers.Contains(name))
            {
                this.waitingUsers.Add(name);
            }
        }
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<string>> GetResultsAsync(string name, string id)
    {
        if (this.results.ContainsKey(name))
        {
            if (this.results[name].ContainsKey(id))
            {
                return Task.FromResult(this.results[name][id].AsEnumerable());
            }
        }
        return Task.FromResult(new List<string>().AsEnumerable());
    }

    /// <inheritdoc/>
    public Task SetResultAsync(string name, string id, string result)
    {
        if (this.results.ContainsKey(name))
        {
            if (this.results[name].ContainsKey(id))
            {
                this.results[name][id].Add(result);
            }
            else
            {
                this.results[name][id] = new() { result };
            }
        }
        else
        {
            this.results[name] = new() { { id, new() { result } } };
        }
        return Task.CompletedTask;
    }
}

