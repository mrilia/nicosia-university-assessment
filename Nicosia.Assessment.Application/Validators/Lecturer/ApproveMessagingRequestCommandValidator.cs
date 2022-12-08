using System;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.ApproveMessagingRequest;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.Validators.Lecturer
{
    public class ApproveMessagingRequestCommandValidator : AbstractValidator<ApproveMessagingRequestCommand>
    {
        private readonly ISectionContext _sectionContext;
        private readonly ILecturerContext _lecturerContext;
        private readonly IApprovalRequestContext _approvalRequestContext;

        public ApproveMessagingRequestCommandValidator(ILecturerContext lecturerContext, ISectionContext sectionContext, IApprovalRequestContext approvalRequestContext)
        {
            _sectionContext = sectionContext;
            _approvalRequestContext = approvalRequestContext;
            _lecturerContext = lecturerContext;

            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.ApprovalRequestId)
                .NotEmpty().WithMessage(ResponseMessage.ApprovalRequestIdIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.ApprovalRequestIdIsRequired).WithErrorCode("999")
                .Must(ApprovalRequestExists).WithMessage(ResponseMessage.ApprovalRequestNotFound).WithErrorCode("201");

            RuleFor(dto => dto.SectionId)
                .NotEmpty().WithMessage(ResponseMessage.SectionIdIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.SectionIdIsRequired).WithErrorCode("999")
                .Must(SectionExists).WithMessage(ResponseMessage.SectionNotFound).WithErrorCode("201");

            RuleFor(dto => dto.LecturerId)
                .NotEmpty().WithMessage(ResponseMessage.LecturerIdIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.LecturerIdIsRequired).WithErrorCode("999")
                .Must(LecturerExists).WithMessage(ResponseMessage.LecturerNotFound);

            RuleFor(dto => dto)
                .Must(RequestNotApprovedBefore).WithMessage(ResponseMessage.RequestApprovedBefore)
                .Must(LecturerLeadsClass).WithMessage(ResponseMessage.LecturerDoesNotLeadClass);
        }


        private bool ApprovalRequestExists(Guid approvalRequestId)
        {
            return _approvalRequestContext.ApprovalRequests.Any(a => a.ApprovalRequestId == approvalRequestId);
        }

        private bool RequestNotApprovedBefore(ApproveMessagingRequestCommand request)
        {
            return !_approvalRequestContext.ApprovalRequests.Any(a => a.ApprovalRequestId == request.ApprovalRequestId && a.Status == ApprovalRequestStatus.Approved);
        }

        private bool LecturerExists(Guid lecturerId)
        {
            return _lecturerContext.Lecturers.Any(a => a.LecturerId == lecturerId);
        }

        private bool SectionExists(Guid sectionId)
        {
            return _sectionContext.Sections.Any(a => a.SectionId == sectionId);
        }

        private bool LecturerLeadsClass(ApproveMessagingRequestCommand request)
        {
            return _sectionContext.Sections
                .Include(i => i.Lecturers)
                .FirstOrDefault(a => a.SectionId == request.SectionId)
                ?.Lecturers?.Any(a =>
                a.LecturerId == request.LecturerId) ?? false;
        }
    }
}
