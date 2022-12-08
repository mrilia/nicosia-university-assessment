using System;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Student.Dto
{
    public class StudentForLecturerDto
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
