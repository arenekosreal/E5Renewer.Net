namespace E5Renewer.Controllers
{
    /// <summary>The struct which stores query result and as a response.</summary>
    public readonly struct InvokeResult
    {
        /// <value>Request method.</value>
        /// <remarks>Is **NOT** HttpContext.Request.Method</remarks>
        public readonly string method { get; }
        /// <value>The params to be passed to request method.</value>
        public readonly Dictionary<string, object?> args { get; }
        /// <value>The request result.</value>
        public readonly object? result { get; }
        /// <value>The timestamp.</value>
        public readonly long timestamp { get; }
        /// <summary>Initialize an <c>InvokeResult</c> from method, params, result and timestamp.</summary>
        /// <param name="method">The request method.</param>
        /// <param name="args">The request params.</param>
        /// <param name="result">The request result.</param>
        /// <param name="timestamp">The request timestamp.</param>
        public InvokeResult(string method, Dictionary<string, object?> args, object? result, long timestamp)
        {
            this.method = method;
            this.args = args;
            this.result = result;
            this.timestamp = timestamp;
        }
    }
}
