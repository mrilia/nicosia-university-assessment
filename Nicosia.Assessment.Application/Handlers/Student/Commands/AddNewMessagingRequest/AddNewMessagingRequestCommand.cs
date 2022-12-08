using MediatR;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Results;
using System;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewMessagingRequest
{
    public class AddNewMessagingRequestCommand : IRequest<Result<ApprovalRequestDto>>
    {
        public Guid StudentId { get; set; }
        public Guid SectionId { get; set; }
        public string Details { get; set; }
    }

    public class AddNewMessagingRequest
    {
        public void SetStudentId(Guid studentId)
        {
            StudentId=studentId;
        }
        public Guid GetStudentId()
        {
            return StudentId;
        }
        private Guid StudentId { get; set; }
        public Guid SectionId { get; set; }
        public string Details { get; set; }
    }
}
