namespace E5Renewer.Models.CommandLine
{
    /// <summary>class to storage commandline parsed results.</summary>
    public class CommandLineParsedResult
    {
        /// <value>If enable systemd mod.</value>
        public readonly bool systemd;

        /// <value> The path to config file.</value>
        public readonly FileInfo config;

        /// <summary>Initialize <c>CommandLineParsedResult</c> with default values.</summary>
        /// <param name="config">The path to config file.</param>
        /// <param name="systemd">If enable systemd mode. Defaults to <c>false</c>.</param>
        public CommandLineParsedResult(FileInfo config, bool systemd = false)
        {
            this.config = config;
            this.systemd = systemd;
        }
    }
}
