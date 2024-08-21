namespace E5Renewer.Models.Config
{
    /// <summary>class to store program config.</summary>
    /// <remarks>For compatibility to Python version.</remarks>
    public sealed record class Config : ICheckable
    {
        /// <value>Authentication token.</value>
        public string authToken { get; set; }

        /// <value>HTTP json api listen address.</value>
        public string listenAddr { get; set; }

        /// <value>HTTP json api listen port.</value>
        public uint listenPort { get; set; }

        /// <value><br/>
        /// Use Unix Domain Socket instead TCP for HTTP json api.<br/>
        /// </value>
        /// <remarks>
        /// Only available when HTTP listen failed and your plaform supports Unix Domain Socket.
        /// </remarks>
        public string listenSocket { get; set; }

        /// <value><br/>
        /// The permission of Unix Domain Socket. <br/>
        /// </value>
        /// <remarks>
        /// In octal number like 777 or 644.
        /// </remarks>
        public uint listenSocketPermission { get; set; }

        /// <value>The list of
        /// <see cref="GraphUser">GraphUser</see>s
        /// to access msgraph apis.</value>
        public List<GraphUser> users { get; set; }

        /// <value>The map of passwords for certificate.</value>
        public Dictionary<string, string?>? passwords { get; set; }

        /// <inheritdoc/>
        public bool isCheckPassed
        {
            get
            {
                return !string.IsNullOrEmpty(this.authToken) &&
                    users.All(
                        (user) => user.isCheckPassed
                    );
            }
        }

        /// <summary>Initialize <c>Config</c> with default values.</summary>
        public Config()
        {
            this.authToken = "";
            this.listenAddr = "127.0.0.1";
            this.listenPort = 8888;
            this.listenSocket = "/run/e5renewer/e5renewer.socket";
            this.listenSocketPermission = 666;
            this.users = new();
        }
    }
}
