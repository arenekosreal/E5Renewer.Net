using System.Text.Json;

using E5Renewer.Controllers;

namespace E5Renewer
{
    internal static class StreamExtends
    {
        public static async Task<JsonAPIV1Request> ToJsonAPIV1RequestAsync(this Stream stream) =>
            await JsonSerializer.DeserializeAsync<JsonAPIV1Request>(
                stream, JsonAPIV1RequestJsonSerializerContext.Default.JsonAPIV1Request
            );
    }
}
