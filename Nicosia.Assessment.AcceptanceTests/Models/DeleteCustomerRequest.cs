using System.Text.Json.Serialization;

namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class DeleteCustomerRequest
    {
        [JsonPropertyName("id")]
        public ulong Id { get; set; }
    }
}
