using Tomlyn;
using System.Globalization;
using E5Renewer.Config;

namespace E5Renewer.Modules.ConfigParsers
{
    [Module]
    public class TomlParser : IConfigParser
    {
        public string name { get => "TomlParser"; }
        public string author { get => "E5Renewer"; }
        public SemVer apiVersion { get => new(0, 1, 0); }
        public bool IsSupported(string path) => path.EndsWith(".toml");
        public async Task<RuntimeConfig> ParseConfig(string path)
        {
            RuntimeConfig runtimeConfig;
            using (StreamReader stream = File.OpenText(path))
            {
                runtimeConfig = Toml.ToModel<RuntimeConfig>(
                await stream.ReadToEndAsync(), path, new()
                {
                    ConvertPropertyName = delegate (string input)
                    {
                        // snake_case to camelCase
                        string[] words = input.Split("_");
                        for (int i = 0; i < words.Count(); i++)
                        {
                            if (i > 0)
                            {
                                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i]);
                            }
                        }
                        return string.Join("", words);

                    }
                }
                );
            }
            return runtimeConfig;
        }
    }
}
