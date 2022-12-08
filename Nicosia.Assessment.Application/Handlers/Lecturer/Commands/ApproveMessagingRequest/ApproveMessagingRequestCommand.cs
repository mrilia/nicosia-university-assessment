using System;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.ApproveMessagingRequest
{
    public class ApproveMessagingRequestCommand : IRequest<Result<ApproveRequestDto>>
    {
        public Guid ApprovalRequestId { get; set; }
        public Guid LecturerId { get; set; }
        public Guid SectionId { get; set; }
    }

    public class ApproveMessagingRequest
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
