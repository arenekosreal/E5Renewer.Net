using System.Collections.Immutable;

using CaseConverter;

using CsToml;

namespace E5Renewer.Models.Secrets.Toml;

internal static class TomlDocumentNodeExtends
{
    public static UserSecret ToUserSecret(this TomlDocumentNode node)
    {
        List<User> users = new();
        TomlDocumentNode usersNode = node[nameof(users).ToSnakeCase()];
        for (int i = 0; i < usersNode.GetArray().Count; i++)
        {
            users.Add(usersNode[i].ToUser());
        }

        Dictionary<string, string>? passwords;
        bool passwordsDeserialized = node[nameof(passwords).ToSnakeCase()].TryGetValue<Dictionary<string, string>>(out passwords);

        return new(
            ImmutableList.CreateRange(users),
            passwordsDeserialized && passwords is not null ? ImmutableDictionary.CreateRange(passwords) : null
        );
    }

    public static User ToUser(this TomlDocumentNode node)
    {
        string name, tenantId, clientId;
        name = node[nameof(name).ToSnakeCase()].GetString();
        tenantId = node[nameof(tenantId).ToSnakeCase()].GetString();
        clientId = node[nameof(clientId).ToSnakeCase()].GetString();

        string? secret;
        secret = node[nameof(secret).ToSnakeCase()].TryGetString(out secret) ? secret : null;

        FileInfo? certificate;
        certificate = node[nameof(certificate).ToSnakeCase()].TryGetString(out string path) ? new(path) : null;

        TimeOnly? fromTime, toTime;
        fromTime = node[nameof(fromTime).ToSnakeCase()].TryGetTimeOnly(out TimeOnly resultFromTime) ? resultFromTime : null;
        toTime = node[nameof(toTime).ToSnakeCase()].TryGetTimeOnly(out TimeOnly resultToTime) ? resultToTime : null;

        List<DayOfWeek>? days;
        bool daysDeserialized = node[nameof(days).ToSnakeCase()].TryGetValue<List<DayOfWeek>>(out days);

        return new(
            name, tenantId, clientId, secret, certificate,
            fromTime, toTime,
            daysDeserialized && days is not null ? ImmutableList.CreateRange(days) : null
        );
    }
}

