namespace E5Renewer.Exceptions
{
    /// <summary>The Exception is thown when no <c>ConfigParser</c> is found for config path given.</summary>
    public class NoParserFoundException : Exception
    {
        /// <summary>Initialize <c>NoParserFoundException</c> from config path.</summary>
        public NoParserFoundException(string configName) : base(string.Format("No Parser found for config {0}", configName)) { }
    }
}
