namespace E5Renewer.Exceptions
{
    /// <summary>The Exception that is thown when config is invalid.</summary>
    public class InvalidConfigException : ArgumentException
    {
        /// <summary>Initialize <c>InvalidConfigException</c> by using message.</summary>
        public InvalidConfigException(string msg) : base(msg) { }
    }
}
