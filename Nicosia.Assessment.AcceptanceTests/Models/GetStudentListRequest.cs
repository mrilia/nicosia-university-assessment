using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class GetStudentListRequest
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
