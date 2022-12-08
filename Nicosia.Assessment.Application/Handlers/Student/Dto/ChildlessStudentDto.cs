using System;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Student.Dto
{
    public class ChildlessStudentDto
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
