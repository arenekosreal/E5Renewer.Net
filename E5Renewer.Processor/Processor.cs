using E5Renewer.Config;

namespace E5Renewer.Processor
{
    public class GraphAPIProcessor : BackgroundService
    {
        private readonly List<GraphUser> users;
        private readonly ILogger<GraphAPIProcessor> logger;
        public GraphAPIProcessor(List<GraphUser> users, ILogger<GraphAPIProcessor> logger)
        {
            this.users = users;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            logger.LogDebug("Processor is starting...");
            List<Task> tasks = new();
            foreach (GraphUser user in users)
            {
                tasks.Add(
                    CreateTaskForUser(cancellationToken, user)
                );
            }
            if (tasks.Count() > 0)
            {
                await Task.WhenAll(tasks.ToArray());
            }
            logger.LogDebug("Processor is stopped.");
        }
        private async Task CreateTaskForUser(CancellationToken cancellationToken, GraphUser user)
        {
            logger.LogDebug(string.Format("Starts task for user {0}", user.name));
            while (!cancellationToken.IsCancellationRequested)
            {
                if (user.enabled)
                {
                    logger.LogDebug("Calling next api for user {0}", user.name);
                    user.SetRunning(true);
                    await user.GetAPICaller().CallNextAPI();
                }
                else
                {
                    logger.LogDebug("Setting user {0} to waiting state", user.name);
                    user.SetRunning(false);
                }
                int millliSeconds = user.milliSecondsUntilNextStarting;
                logger.LogDebug("Waiting {0}ms for next calls...", millliSeconds);
                await Task.Delay(millliSeconds);
            }
            logger.LogDebug(string.Format("Task for user {0} has been stopped.", user.name));
        }

    }
}
