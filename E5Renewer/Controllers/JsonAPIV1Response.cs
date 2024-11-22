using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace E5Renewer.Controllers
{
    /// <summary>Readonly struct to store json api response.</summary>
    public readonly struct JsonAPIV1Response
    {
        /// <value>If this protocol is valid.</value>
        [JsonIgnore]
        public bool valid { get => timestamp > 0 && !string.IsNullOrEmpty(method) && string.IsNullOrWhiteSpace(method); }

        /// <value>The timestamp.</value>
        public long timestamp { get; }

        /// <value>Request method.</value>
        /// <remarks>Is **NOT** HttpContext.Request.Method</remarks>
        public string method { get; }

        /// <value>The request result.</value>
        public object? result { get; }

        /// <value>The params to be passed to request method.</value>
        public ReadOnlyDictionary<string, string?> args { get; }

        /// <summary>Initialize <see cref="JsonAPIV1Response"/> with arguments given.</summary>
        [JsonConstructor]
        public JsonAPIV1Response(long timestamp, string method, object? result, ReadOnlyDictionary<string, string?> args) =>
            (this.timestamp, this.method, this.result, this.args) = (timestamp, method, result, args);
    }

    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
        IncludeFields = true
    )]
    [JsonSerializable(typeof(JsonAPIV1Response))]
    [JsonSerializable(typeof(IEnumerable<string>))]
    [JsonSerializable(typeof(IEnumerable<object>))]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(object))]
    internal partial class JsonAPIV1ResponseJsonSerializerContext : JsonSerializerContext { }
}
