using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

namespace E5Renewer.Controllers.V1;

/// <summary>Deseialized Json api request v1.</summary>
public readonly struct JsonAPIV1Request : IJsonRequest
{
    /// <value>If this protocol is valid.</value>
    [JsonIgnore]
    public bool valid { get => timestamp > 0 && !string.IsNullOrEmpty(method) && string.IsNullOrWhiteSpace(method); }

    /// <value>The timestamp.</value>
    public long timestamp { get; }

    /// <value>Request method.</value>
    /// <remarks>Is **NOT** HttpContext.Request.Method</remarks>
    public string? method { get; }

    /// <value>The params to be passed to request method.</value>
    public ReadOnlyDictionary<string, string?> args { get; }

    /// <summary>Initialize <see cref="JsonAPIV1Request"/> with arguments given.</summary>
    [JsonConstructor]
    public JsonAPIV1Request(long timestamp, string? method, ReadOnlyDictionary<string, string?> args) =>
        (this.timestamp, this.method, this.args) = (timestamp, method, args);
}

/// <summary>Json serializer context for <see cref="JsonAPIV1Request"/></summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    IncludeFields = true
)]
[JsonSerializable(typeof(JsonAPIV1Request))]
public partial class JsonAPIV1RequestJsonSerializerContext : JsonSerializerContext { }
