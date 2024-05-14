﻿using System.Globalization;
using Tomlyn;
using E5Renewer.Models.Config;
using E5Renewer.Models.Modules;
using E5Renewer.Models;

namespace E5Renewer.Modules.TomlParser;

/// <summary>
/// Parse toml to
/// <see cref="Config">Config</see>.
/// </summary>
[Module]
public class TomlParser : IConfigParser
{
    /// <inheritdoc/>
    public string name { get => nameof(TomlParser); }

    /// <inheritdoc/>
    public string author { get => "E5Renewer"; }

    /// <inheritdoc/>
    public SemanticVersioning.Version apiVersion
    {
        get => typeof(TomlParser).Assembly.GetName().Version?.ToSemanticVersion() ?? new(0, 1, 0);
    }

    /// <inheritdoc/>
    public bool IsSupported(string path) => path.EndsWith(".toml");

    /// <inheritdoc/>
    public async Task<Config> ParseConfigAsync(string path)
    {
        Config runtimeConfig;
        using (StreamReader stream = File.OpenText(path))
        {
            runtimeConfig = Toml.ToModel<Config>(
                await stream.ReadToEndAsync(), path, new()
                {
                    ConvertPropertyName = (input) =>
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
