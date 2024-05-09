using E5Renewer.Config;

namespace E5Renewer.Processor
{
    public static class UsersManager
    {
        private static readonly List<string> waitingUsers = new();
        private static readonly List<string> runningUsers = new();
        internal static void SetRunning(this GraphUser user, bool running)
        {
            if (running)
            {
                if (waitingUsers.Contains(user.name))
                {
                    waitingUsers.Remove(user.name);
                }
                if (!runningUsers.Contains(user.name))
                {
                    runningUsers.Add(user.name);
                }
            }
            else
            {
                if (!waitingUsers.Contains(user.name))
                {
                    waitingUsers.Add(user.name);
                }
                if (runningUsers.Contains(user.name))
                {
                    runningUsers.Remove(user.name);
                }
            }
        }
        public static List<string> GetRunningUsers() => runningUsers;
        public static List<string> GetWaitingUsers() => waitingUsers;
    }
}
