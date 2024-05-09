namespace E5Renewer.Config
{
    public class RuntimeConfig : ICheckable
    {
        public bool debug { get; set; }
        public string authToken { get; set; }
        public string listenAddr { get; set; }
        public uint listenPort { get; set; }
        public string listenSocket { get; set; }
        public uint listenSocketPermission { get; set; }
        public List<GraphUser> users { get; set; }
        public bool check
        {
            get
            {
                return this.authToken != "" &&
                    users.All(
                        (GraphUser user) => user.check
                    );
            }
        }
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
