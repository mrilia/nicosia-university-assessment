using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class DeleteStudentRequest
    {
        [JsonPropertyName("studentId")]
        public Guid StudentId { get; set; }
    }
}
