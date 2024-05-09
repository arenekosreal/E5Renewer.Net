namespace E5Renewer.Exceptions
{
    public class InvalidConfigException : ArgumentException
    {
        public InvalidConfigException(string msg) : base(msg) { }
    }
}
