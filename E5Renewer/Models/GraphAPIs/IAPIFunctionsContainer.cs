namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>The api interfact to get functions calling msgraph apis.</summary>
    public interface IAPIFunctionsContainer
    {
        /// <summary>Get all functions calling msgraph apis in the class.</summary>
        public IEnumerable<KeyValuePair<string, APIFunction>> GetAPIFunctions();
    }
}
