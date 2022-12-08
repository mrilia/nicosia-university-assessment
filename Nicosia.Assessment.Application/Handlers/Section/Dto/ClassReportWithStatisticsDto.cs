using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Section.Dto
{
    public class ClassReportWithStatisticsDto : ChildlessClassDto
    {
        public int StudentCount { get; set; }
    }
}
