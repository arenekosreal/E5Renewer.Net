using E5Renewer.Models.Config;
using E5Renewer.Models.GraphAPIs;
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
        private readonly IEnumerable<GraphUser> users;
        private readonly IServiceProvider serviceProvider;

        /// <summary>Initialize <c>PrepareUsersService</c> with parameters.</summary>
        /// <param name="logger">The logger to create log.</param>
        /// <param name="users">The <see cref="GraphUser">GraphUser</see>s to process.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider">IServiceProvider</see> implementation.</param>
        /// <remarks>All parameters should be injected by AspNet.Core.</remarks>
        public PrepareUsersService(ILogger<PrepareUsersService> logger, IEnumerable<GraphUser> users, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.users = users;
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken token)
        {
            IStatusManager statusManager = this.serviceProvider.GetRequiredService<IStatusManager>();
            foreach (GraphUser user in this.users)
            {
                await this.DoAPICallForUser(token, user, statusManager);
            }

        }

        private async Task DoAPICallForUser(CancellationToken token, GraphUser user, IStatusManager statusManager)
        {
            IGraphAPICaller apiCaller = this.serviceProvider.GetRequiredKeyedService<IGraphAPICaller>(user);
            while (!token.IsCancellationRequested)
            {
                await statusManager.SetUserStatusAsync(user.name, user.enabled);
                if (user.enabled)
                {
                    await apiCaller.CallNextAPIAsync(user);
                    await apiCaller.CalmDownAsync(user);
                }
                else
                {
                    await Task.Delay(user.timeToStart); ;
                }
            }
        }
    }
}
