using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ChildlessClassDto
    {
        [JsonIgnore]
        public Guid SectionId { get; set; }
        public string Number { get; set; }
        public string Details { get; set; }
    }
}
