using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class UpdateStudentRequest
    {
        [JsonPropertyName("id")]
        public ulong Id { get; set; }
        
        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string? Lastname { get; set; }

        [JsonPropertyName("dateofbirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("phonenumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
