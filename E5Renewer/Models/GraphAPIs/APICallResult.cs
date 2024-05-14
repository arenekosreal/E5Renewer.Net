namespace E5Renewer.Models.GraphAPIs
{
    /// <summary>The struct stores result of api request.</summary>
    public readonly struct APICallResult
    {
        private const string connector = "-";

        /// <value>The <c>APICallResult</c> shows an error result.</value>
        public static readonly APICallResult errorResult = new(-1, "ERROR");

        /// <value>The response code.</value>
        /// <remarks><c>-1</c> means failed to send request.</remarks>
        public readonly int code;

        /// <value>The response message.</value>
        /// <remarks><c>ERROR</c> means failed to send request.</remarks>
        public readonly string msg;

        /// <value>The raw result of msgraph api request.</value>
        public readonly object? rawResult;

        /// <summary>Initialize a <c>APICallResult</c> by code, message and api request result.</summary>
        /// <param name="code">The response code.</param>
        /// <param name="msg">The response message.</param>
        /// <param name="rawResult">The result of request.</param>
        public APICallResult(int code = 200, string msg = "OK", object? rawResult = null)
        {
            this.code = code;
            this.msg = msg;
            this.rawResult = rawResult;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Join(connector, new object[2] { code, msg });
        }

        /// <summary>Convert <c cref="rawResult">rawResult</c> to type given.</summary>
        /// <typeparam name="T">The type to convert.</typeparam>
        /// <returns>The <c cref="rawResult">rawResult</c> in type <typeparamref name="T">T</typeparamref>. null if failed to convert.
        /// </returns>
        public T? CastRawResultTo<T>()
            where T : class
        {
            return this.rawResult as T;
        }
    }
}
