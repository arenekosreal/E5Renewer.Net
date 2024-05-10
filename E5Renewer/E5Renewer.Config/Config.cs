namespace E5Renewer.Config
{
    /// <summary>Runtime used Config.</summary>
    public class RuntimeConfig : ICheckable
    {
        /// <value>Enable debug mode.</value>
        public bool debug { get; set; }
        /// <value>Authentication token.</value>
        public string authToken { get; set; }
        /// <value>HTTP json api listen address.</value>
        public string listenAddr { get; set; }
        /// <value>HTTP json api listen port.</value>
        public uint listenPort { get; set; }
        /// <value><br/>
        /// Use Unix Domain Socket instead TCP for HTTP json api.<br/>
        /// <remarks>
        /// Only available when HTTP listen failed and your plaform supports Unix Domain Socket.
        /// </remarks>
        /// </value>
        public string listenSocket { get; set; }
        /// <value><br/>
        /// The permission of Unix Domain Socket. <br/>
        /// <remarks>
        /// In octal number like 777 or 644.
        /// </remarks>
        /// </value>
        public uint listenSocketPermission { get; set; }
        /// <value>The list of users to access msgraph apis.</value>
        /// <seealso cref="GraphUser"/>
        public List<GraphUser> users { get; set; }
        /// <value>If this object is valid.</value>
        /// <seealso cref="ICheckable"/>
        public bool check
        {
            get
            {
                return !string.IsNullOrEmpty(this.authToken) &&
                    users.All(
                        (GraphUser user) => user.check
                    );
            }
        }
        /// <summary>
        /// Default constructor of <c>RuntimeConfig</c>
        /// </summary>
        public RuntimeConfig()
        {
            this.debug = false;
            this.authToken = "";
            this.listenAddr = "127.0.0.1";
            this.listenPort = 8888;
            this.listenSocket = "/run/e5renewer/e5renewer.socket";
            this.listenSocketPermission = 666;
            this.users = new();
        }
    }
}
