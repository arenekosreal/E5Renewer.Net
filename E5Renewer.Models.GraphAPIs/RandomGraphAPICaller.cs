

using E5Renewer.Models.Modules;
using E5Renewer.Models.Secrets;
using E5Renewer.Models.Statistics;

using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace E5Renewer.Models.GraphAPIs;

/// <summary>
/// <see cref="IGraphAPICaller">IGraphAPICaller</see>
/// implementation which calls msgraph apis randomly.
/// </summary>
[Module]
public class RandomGraphAPICaller : BasicModule, IGraphAPICaller
{
    private readonly ILogger<RandomGraphAPICaller> logger;
    private readonly IEnumerable<IAPIFunction> apiFunctions;
    private readonly IStatusManager statusManager;
    private readonly IUserClientProvider clientProvider;

    /// <summary>Initialize <c>RandomGraphAPICaller</c> with parameters given.</summary>
    /// <param name="logger">The logger to generate log.</param>
    /// <param name="apiFunctions">All known api functions with their id.</param>
    /// <param name="statusManager"><see cref="IStatusManager"/> implementation.</param>
    /// <param name="clientProvider"><see cref="IUserClientProvider"/> implementation.</param>
    /// <remarks>All parameters should be injected by Asp.Net Core.</remarks>
    public RandomGraphAPICaller(
        ILogger<RandomGraphAPICaller> logger,
        IEnumerable<IAPIFunction> apiFunctions,
        IStatusManager statusManager,
        IUserClientProvider clientProvider
    )
    {
        this.logger = logger;
        this.apiFunctions = apiFunctions;
        this.statusManager = statusManager;
        this.clientProvider = clientProvider;
        this.logger.LogDebug("Found {0} api functions", this.apiFunctions.Count());
    }

    /// <inheritdoc/>
    public async Task CallNextAPIAsync(User user)
    {
        if (this.apiFunctions.Count() <= 0)
        {
            this.logger.LogError("No {0} is found.", nameof(IAPIFunction));
            return;
        }


        int GetFunctionWeightOfCurrentUser(IAPIFunction function)
        {
            string successAPICallResult = new APICallResult().ToString();
            IEnumerable<string> results = this.statusManager.GetResultsAsync(user.name, function.id).Result;
            int successCount = results.Count((item) => item == successAPICallResult);
            return successCount + 1; // let weight greater than zero
        }

        GraphServiceClient client = await this.clientProvider.GetClientForUserAsync(user);
        IAPIFunction apiFunction = this.apiFunctions.GetDifferentItemsByWeight(GetFunctionWeightOfCurrentUser, 1).First();
        APICallResult result = await apiFunction.SafeCallAsync(client, user.name);
        await this.statusManager.SetResultAsync(user.name, apiFunction.id, result.ToString());
    }

    /// <inheritdoc/>
    public async Task CalmDownAsync(CancellationToken token, User user)
    {
        if (user.timeToStart == TimeSpan.Zero)
        {
            const int calmDownMinMilliSeconds = 300000;
            const int calmDownMaxMilliSeconds = 500000;
            Random random = new();
            int milliseconds = random.Next(calmDownMinMilliSeconds, calmDownMaxMilliSeconds);
            this.logger.LogDebug("{0} is going to sleep for {1} millisecond(s)", user.name, milliseconds);
            await Task.Delay(milliseconds, token);
        }
    }
}

