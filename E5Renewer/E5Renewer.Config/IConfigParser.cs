using E5Renewer.Modules;

namespace E5Renewer.Config
{
    /// <summary>The api interface of <c>ConfigParser</c>.</summary>
    /// <seealso cref="IModule"/>
    public interface IConfigParser : IModule
    {
        /// <summary>If <paramref name="path"/> given is supported by the parser.</summary>
        /// <param name="path">The path of config file.</param>
        /// <returns>If the path given is supported.</returns>
        public bool IsSupported(string path);
        /// <summary>Parse <paramref name="path"/> and returns parsed result.</summary>
        /// <param name="path">The path of config file.</param>
        /// <returns>The parsed result.</returns>
        public Task<RuntimeConfig> ParseConfig(string path);
    }
}
