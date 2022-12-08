using System;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Domain.Models.ApprovalRequests
{
    public enum ApprovalRequestStatus
    {
        Waiting,
        Approved,
        Rejected
    }
    public class ApprovalRequest
    {
        public Guid ApprovalRequestId { get; set; }
        public ApprovalRequestStatus Status { get; set; }
        public string Details { get; set; }
        public Guid StudentId { get; set; }
        public Guid SectionId { get; set; }
        public DateTime LastChange { get; set; }
        public Guid? LecturerId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Lecturer? Lecturer { get; set; }
        public virtual Section.Section Section { get; set; }

    }
}