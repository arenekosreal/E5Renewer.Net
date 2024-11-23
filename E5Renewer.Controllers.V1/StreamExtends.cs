using System.Text.Json;


namespace E5Renewer.Controllers.V1;

/// <summary>Extension methods to <see cref="Stream"/></summary>
public static class StreamExtends
{
    /// <summary> Read stream and parse to <see cref="JsonAPIV1Request"/></summary>
    public static async Task<JsonAPIV1Request> ToJsonAPIV1RequestAsync(this Stream stream) =>
        await JsonSerializer.DeserializeAsync<JsonAPIV1Request>(
            stream, JsonAPIV1RequestJsonSerializerContext.Default.JsonAPIV1Request
        );
}

