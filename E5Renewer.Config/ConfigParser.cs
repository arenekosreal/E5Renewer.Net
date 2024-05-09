using E5Renewer.Modules;
using E5Renewer.Exceptions;

namespace E5Renewer.Config
{
    public static class ConfigParser
    {
        private static readonly List<IConfigParser> parsers = ModulesLoader.GetRegisteredModules<IConfigParser>();
        private static RuntimeConfig? parsedConfig;
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
