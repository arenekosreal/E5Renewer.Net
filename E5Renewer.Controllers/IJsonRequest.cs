using System.Text.Json.Serialization;

namespace E5Renewer.Controllers;

/// <summary>Basic deserialized request.</summary>
public interface IJsonRequest { }

[JsonSerializable(typeof(IJsonRequest))]
internal partial class JsonRequestJsonSerializerContext : JsonSerializerContext { }
