using System.Text.Json.Serialization;

namespace E5Renewer.Models.Secrets.Json;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    IncludeFields = true
)]
[JsonSerializable(typeof(JsonSerializableUserSecret))]
internal partial class LoaderJsonSerializerContext : JsonSerializerContext { }

