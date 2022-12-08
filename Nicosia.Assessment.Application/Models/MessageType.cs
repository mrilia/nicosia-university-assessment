using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageType
{
    Success,
    Warning,
    Error
}