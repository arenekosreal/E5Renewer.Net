using E5Renewer.Config;

namespace E5Renewer.Processor
{
    public static class ResultsManager
    {
        private static readonly Dictionary<string, List<KeyValuePair<string, string>>> results = new();
        internal static void UpdateResult(this GraphUser user, APICallResult result, string apiName)
        {
            if (results.ContainsKey(user.name))
            {
                results[user.name].Add(new(apiName, result.ToString()));
            }
            else
            {
                results[user.name] = new() { new(apiName, result.ToString()) };
            }
        }
        public static List<string> GetResults(string userName, string apiName)
        {
            List<KeyValuePair<string, string>> kvs = results.ContainsKey(userName) ? results[userName] : new();
            return kvs.Where((kv) => kv.Key == apiName).Select((kv) => kv.Value).ToList();
        }
    }
}
