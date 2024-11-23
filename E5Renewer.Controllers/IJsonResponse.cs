using System.Text.Json.Serialization;

namespace E5Renewer.Controllers;

/// <summary>Basic deserialized response.</summary>
public interface IJsonResponse { }

[JsonSerializable(typeof(IJsonResponse))]
internal partial class JsonResponseJsonSerializerContext : JsonSerializerContext { }
