using System.Text.Json;
using System.Text.Json.Serialization;

namespace E5Renewer.Models.Secrets.Json;

/// <summary>Custom <see cref="JsonConverter"/> to convert between <see cref="string"/> and <see cref="FileInfo"/>.</summary>
internal class FileInfoOrNullConverter : JsonConverter<FileInfo?>
{
    /// <inheritdoc/>
    public override FileInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? read = reader.GetString();
        return read is not null ? new(read) : null;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, FileInfo? value, JsonSerializerOptions options) => writer.WriteStringValue(value?.FullName);

}

