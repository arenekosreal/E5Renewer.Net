namespace E5Renewer.Exceptions
{
    public class NoParserFoundException : Exception
    {
        public NoParserFoundException(string configName) : base(string.Format("No Parser found for config {0}", configName)) { }
    }
}
