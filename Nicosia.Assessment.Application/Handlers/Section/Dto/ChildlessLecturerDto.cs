using System;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ChildlessLecturerDto
    {
        [JsonIgnore]
        public Guid LecturerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}