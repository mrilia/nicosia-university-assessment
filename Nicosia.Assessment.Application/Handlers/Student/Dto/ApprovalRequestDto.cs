using Nicosia.Assessment.Domain.Models.ApprovalRequests;
using System;
using System.Text.Json.Serialization;

namespace Nicosia.Assessment.Application.Handlers.Student.Dto
{
    public class ApprovalRequestDto
    {
        public Guid ApprovalRequestId { get; set; }
        public ApprovalRequestStatus Status { get; set; }
        public string Details { get; set; }
        public DateTime LastChange { get; set; }

        [JsonIgnore]
        public Guid StudentId { get; set; }
        
        [JsonIgnore]
        public Guid SectionId { get; set; }
        
        [JsonIgnore]
        public Guid LecturerId { get; set; }

        [JsonIgnore]
        public virtual Domain.Models.User.Student Student { get; set; }
        
        [JsonIgnore]
        public virtual Domain.Models.User.Lecturer Lecturer { get; set; }
        
        [JsonIgnore]
        public virtual Domain.Models.Section.Section Section { get; set; }
    }
}
