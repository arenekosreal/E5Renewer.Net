namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>Attribute that marks a function calling msgraph apis.</summary>
    public class APIAttribute : Attribute
    {
        /// <value>The id of target function.</value>
        public readonly string id;

        /// <summary>Initialize <c>APIAttribute</c> with parameters given.</summary>
        /// <param name="id">The id of target function.</param>
        public APIAttribute(string id)
        {
            this.id = id;
        }
    }
}
