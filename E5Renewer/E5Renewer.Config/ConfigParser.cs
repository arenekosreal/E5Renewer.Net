using E5Renewer.Modules;
using E5Renewer.Exceptions;

namespace E5Renewer.Config
{
    /// <summary>Utils for parsing config.</summary>
    public static class ConfigParser
    {
        private static readonly List<IConfigParser> parsers = ModulesLoader.GetRegisteredModules<IConfigParser>();
        private static RuntimeConfig? parsedConfig;
        /// <summary>Parse config.</summary>
        /// <param name="fileInfo">The config path in <c>FileInfo</c> object.</param>
        /// <returns>The result of parsed config.</returns>
        public static async Task<RuntimeConfig> ParseConfig(FileInfo fileInfo)
        {
            if (ConfigParser.parsedConfig != null)
            {
                return (RuntimeConfig)ConfigParser.parsedConfig;
            }
            foreach (IConfigParser parser in parsers)
            {
                if (parser.IsSupported(fileInfo.FullName))
                {
                    ConfigParser.parsedConfig = await parser.ParseConfig(fileInfo.FullName);
                    return (RuntimeConfig)ConfigParser.parsedConfig;
                }
            }
            throw new NoParserFoundException(fileInfo.FullName);
        }
    }
}
