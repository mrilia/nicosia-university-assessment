using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class CreateStudentRequest 
    {
        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }
        
        [JsonPropertyName("lastname")]
        public string? Lastname { get; set; }
        
        [JsonPropertyName("dateofbirth")]
        public string? DateOfBirth { get; set; }
        
        [JsonPropertyName("phonenumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("email")] 
        public string? Email { get; set; }

        [JsonPropertyName("password")] 
        public string? Password { get; set; }
    }
}
