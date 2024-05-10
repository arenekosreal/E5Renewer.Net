namespace E5Renewer.Exceptions
{
    /// <summary>The Exception is thown when generic runtime error happend.</summary>
    public class RuntimeException : Exception
    {
        /// <summary>Initialize <c>RuntimeException</c> from message.</summary>
        public RuntimeException(string msg) : base(msg) { }
    }
}
