namespace E5Renewer.Processor
{
    /// <summary>The attribute to mark functions calling msgraph apis.</summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class APIAttribute : Attribute
    {
        /// <value>The name of the api function.</value>
        public string name { get; private set; }
        /// <summary>Initialize an <c>APIAttribute</c> by name.</summary>
        /// <param name="name">The name of the api function.</param>
        public APIAttribute(string name)
        {
            this.name = name;
        }
    }
}
