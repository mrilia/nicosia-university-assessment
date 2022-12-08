using System;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewMessagingRequest;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Validators;

namespace Nicosia.Assessment.Application.Validators.Student
{
    public class AddNewSMessagingRequestCommandValidator : AbstractValidator<AddNewMessagingRequestCommand>
    {
        private readonly ISectionContext _sectionContext;
        private readonly IStudentContext _studentContext;
        private readonly IApprovalRequestContext _approvalRequestContext;

        public AddNewSMessagingRequestCommandValidator(IStudentContext studentContext, ISectionContext sectionContext, IApprovalRequestContext approvalRequestContext)
        {
            _sectionContext = sectionContext;
            _approvalRequestContext = approvalRequestContext;
            _studentContext = studentContext;

            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.SectionId)
                .NotEmpty().WithMessage(ResponseMessage.SectionIdIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.SectionIdIsRequired).WithErrorCode("999")
                .Must(SectionExists).WithMessage(ResponseMessage.SectionNotFound).WithErrorCode("201");

            RuleFor(dto => dto.StudentId)
                .NotEmpty().WithMessage(ResponseMessage.StudentIdIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.StudentIdIsRequired).WithErrorCode("999")
                .Must(StudentExists).WithMessage(ResponseMessage.StudentNotFound);

            RuleFor(dto => dto)
                .Must(RequestNotAddedBefore).WithMessage(ResponseMessage.RequestAddedBefore)
                .Must(StudentIsMemberOfClass).WithMessage(ResponseMessage.StudentIsNotMemberOfClass);
        }


        private bool RequestNotAddedBefore(AddNewMessagingRequestCommand request)
        {
            return !_approvalRequestContext.ApprovalRequests.Any(a => a.StudentId == request.StudentId && a.SectionId == request.SectionId);
        }

        private bool StudentExists(Guid sectionId)
        {
            return _studentContext.Students.Any(a => a.StudentId == sectionId);
        }

        private bool SectionExists(Guid sectionId)
        {
            return _sectionContext.Sections.Any(a => a.SectionId == sectionId);
        }

        private bool StudentIsMemberOfClass(AddNewMessagingRequestCommand request)
        {
            return _sectionContext.Sections
                .Include(i => i.Students)
                .FirstOrDefault(a => a.SectionId == request.SectionId)
                ?.Students?.Any(a =>
                a.StudentId == request.StudentId) ?? false;
        }
    }
}
