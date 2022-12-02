using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class CreateCustomerRequest 
    {
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
        
        [JsonPropertyName("bankaccountnumber")]
        public string? BankAccountNumber { get; set; }
    }
}
