using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models.PaginationResponse;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageType
{
    Success,
    Warning,
    Error
}