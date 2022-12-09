using System;
using System.Text.Json.Serialization;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Dto
{
    public class MessagingRequestDto
    {
        public Guid ApprovalRequestId { get; set; }
        public string Details { get; set; }
        public ApprovalRequestStatus Status { get; set; }
        public DateTime LastChange { get; set; }

        [JsonIgnore]
        public Guid StudentId { get; set; }
        
        [JsonIgnore]
        public Guid SectionId { get; set; }
        
        [JsonIgnore]
        public Guid LecturerId { get; set; }

        public virtual ChildlessStudentDto Student { get; set; }
        
        public virtual  ChildlessLecturerDto Lecturer { get; set; }
        
        public virtual ChildlessClassDto Section { get; set; }
    }
}
