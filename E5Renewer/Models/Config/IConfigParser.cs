using E5Renewer.Models.Modules;

namespace E5Renewer.Models.Config
{
    /// <summary>The api interface for config parser.</summary>
    public interface IConfigParser : IModule
    {
        /// <summary>If this parser supports path given.</summary>
        /// <param name="path">The path to the config.</param>
        public bool IsSupported(string path);

        /// <summary>If this parser supports path given.</summary>
        /// <param name="fileInfo">The <see cref="FileInfo">FileInfo</see> to the config.</param>
        public bool IsSupported(FileInfo fileInfo)
        {
            return IsSupported(fileInfo.FullName);
        }

        /// <summary>Parse config.</summary>
        /// <param name="path">The path to the config.</param>
        /// <returns>Parsed result.</returns>
        public ValueTask<Config> ParseConfigAsync(string path);

        /// <summary>Parse config.</summary>
        /// <param name="fileInfo">The <see cref="FileInfo">FileInfo</see> to the config.</param>
        /// <returns>Parsed result.</returns>
        public async ValueTask<Config> ParseConfigAsync(FileInfo fileInfo)
        {
            return await this.ParseConfigAsync(fileInfo.FullName);
        }
    }
}
