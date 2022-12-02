using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class GetCustomerListRequest
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
