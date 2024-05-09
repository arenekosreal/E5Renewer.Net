namespace E5Renewer.Statistics
{
    public readonly struct InvokeResult
    {
        public readonly string method { get; }
        public readonly Dictionary<string, object?> args { get; }
        public readonly object? result { get; }
        public readonly long timestamp { get; }
        public InvokeResult(string method, Dictionary<string, object?> args, object? result, long timestamp)
        {
            this.method = method;
            this.args = args;
            this.result = result;
            this.timestamp = timestamp;
        }
    }
}
