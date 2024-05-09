namespace E5Renewer.Processor
{
    public struct APICallResult
    {
        private const string connector = "-";
        public static readonly APICallResult errorResult = new(-1, "ERROR");
        public int code;
        public string msg;
        public object? rawResult;
        public APICallResult(int code = 200, string msg = "OK", object? rawResult = null)
        {
            this.code = code;
            this.msg = msg;
            this.rawResult = rawResult;
        }
        public override string ToString()
        {
            return string.Join(connector, new object[2] { code, msg });
        }
        public T? CastRawResultTo<T>()
            where T : class
        {
            return this.rawResult as T;
        }
    }
}
