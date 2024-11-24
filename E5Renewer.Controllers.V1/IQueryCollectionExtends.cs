using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace E5Renewer.Controllers.V1;

internal static class IQueryCollectionExtends
{
    public static JsonAPIV1Request ToJsonAPIV1Request(this IQueryCollection queryCollection)
    {
        StringValues value;
        long timestamp = queryCollection.TryGetValue(nameof(timestamp), out value) &&
                         long.TryParse(value, out long parsedValue)
                         ? parsedValue : default;
        string method;
        if (queryCollection.TryGetValue(nameof(method), out value))
        {
            method = value.FirstOrDefault() ?? string.Empty;
        }
        else
        {
            method = string.Empty;
        }

        Dictionary<string, string?> args =
            queryCollection.TakeWhile((kv) => kv.Key != nameof(timestamp) && kv.Key != nameof(method))
                            .Select((kv) => new KeyValuePair<string, string?>(kv.Key, kv.Value.FirstOrDefault()))
                            .ToDictionary();

        return new(timestamp, method, new(args));
    }
}

