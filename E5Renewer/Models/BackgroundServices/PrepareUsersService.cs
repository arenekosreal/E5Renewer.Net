using E5Renewer.Models.GraphAPIs;
using E5Renewer.Models.Secrets;
using E5Renewer.Models.Statistics;

namespace E5Renewer.Models.BackgroundServices
{
    /// <summary>
    /// <see cref="BackgroundService">BackgroundService</see>
    /// for creating scopes for users.
    /// </summary>
    public class PrepareUsersService : BackgroundService
    {
        private readonly ILogger<PrepareUsersService> logger;
        private readonly ISecretProvider secretProvider;
        private readonly IStatusManager statusManager;
        private readonly IEnumerable<IGraphAPICaller> apiCallers;
        private readonly Dictionary<User, IGraphAPICaller> callerCache = new();

        /// <summary>Initialize <c>PrepareUsersService</c> with parameters.</summary>
        /// <param name="logger">The logger to create log.</param>
        /// <param name="secretProvider">The <see cref="ISecretProvider"/> implicit.</param>
        /// <param name="statusManager">The <see cref="IStatusManager"/> implementation.</param>
        /// <param name="apiCallers">A series of <see cref="IGraphAPICaller"/> implementations.</param>
        /// <remarks>All parameters should be injected by AspNet.Core.</remarks>
        public PrepareUsersService(
            ILogger<PrepareUsersService> logger,
            ISecretProvider secretProvider,
            IStatusManager statusManager,
            IEnumerable<IGraphAPICaller> apiCallers
        )
        {
            this.logger = logger;
            this.secretProvider = secretProvider;
            this.statusManager = statusManager;
            this.apiCallers = apiCallers;
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken token)
        {
            IEnumerable<User> users =
                (await this.secretProvider.GetUserSecretAsync()).users;
            // TODO: Parallel?
            foreach (User user in users)
            {
                while (!token.IsCancellationRequested && user.valid)
                {
                    bool enabled = user.timeToStart == TimeSpan.Zero;
                    await this.statusManager.SetUserStatusAsync(user.name, enabled);
                    if (enabled)
                    {
                        if (!this.callerCache.ContainsKey(user))
                        {
                            Random random = new();
                            this.callerCache[user] = random.GetItems(this.apiCallers.ToArray(), 1)[0];
                        }
                        await this.callerCache[user].CallNextAPIAsync(user);
                        await this.callerCache[user].CalmDownAsync(token, user);
                    }
                    else
                    {
                        TimeSpan delay = user.timeToStart;
                        this.logger.LogDebug(
                            "Sleeping for {0} day(s), {1} hour(s), {2} miniute(s), {3} second(s) and {4} millisecond(s) to wait starting user {5}...",
                                delay.Days, delay.Hours, delay.Minutes, delay.Seconds, delay.Milliseconds, user.name
                        );
                        await Task.Delay(delay, token);
                    }
                }
            }

        }
    }
}
