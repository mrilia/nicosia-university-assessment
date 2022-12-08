using System;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.RejectMessagingRequest
{
    public class RejectMessagingRequestCommand : IRequest<Result<RejectRequestDto>>
    {
        public Guid ApprovalRequestId { get; set; }
        public Guid LecturerId { get; set; }
        public Guid SectionId { get; set; }
    }

    public class RejectMessagingRequest
    {
        public void SetLecturerId(Guid studentId)
        {
            LecturerId=studentId;
        }
        public Guid GetLecturerId()
        {
            return LecturerId;
        }
        public Guid ApprovalRequestId { get; set; }
        private Guid LecturerId { get; set; }
    }
}
