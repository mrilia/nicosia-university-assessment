using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class GetStudentRequest
    {
        [JsonPropertyName("studentId")]
        public Guid StudentId { get; set; }
    }
}
