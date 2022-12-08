using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.UpdateLecturer;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Application.Validators.Lecturer;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.ApproveMessagingRequest
{
    public class ApproveMessagingRequestCommandHandler : IRequestHandler<ApproveMessagingRequestCommand, Result<ApproveRequestDto>>
    {
        private readonly IApprovalRequestContext _approvalRequestContext;
        private readonly ILecturerContext _lecturerContext;
        private readonly ISectionContext _sectionContext;
        private readonly IMapper _mapper;

        public ApproveMessagingRequestCommandHandler(IApprovalRequestContext approvalRequestContext,
            ILecturerContext lecturerContext,
            ISectionContext sectionContext,
            IMapper mapper)
        {
            _approvalRequestContext = approvalRequestContext;
            _lecturerContext = lecturerContext;
            _sectionContext = sectionContext;
            _mapper = mapper;
        }

        public async Task<Result<ApproveRequestDto>> Handle(ApproveMessagingRequestCommand request, CancellationToken cancellationToken)
        {
            ApproveMessagingRequestCommandValidator customerValidator = new ApproveMessagingRequestCommandValidator(_lecturerContext, _sectionContext, _approvalRequestContext);
            var validatorResult = customerValidator.Validate(request);

            if (!validatorResult.IsValid)
                return Result<ApproveRequestDto>.Failed(new BadRequestObjectResult
                    (new ApiMessage(validatorResult.Errors.First().ErrorMessage)));

            var approvalRequestToApprove = await GetAprovalRequestAsync(request, cancellationToken);

            approvalRequestToApprove.Status = ApprovalRequestStatus.Approved;
            approvalRequestToApprove.LecturerId = request.LecturerId;
            approvalRequestToApprove.LastChange = DateTime.Now;

            await _approvalRequestContext.SaveAsync(cancellationToken);

            return Result<ApproveRequestDto>.SuccessFul(_mapper.Map<ApproveRequestDto>(approvalRequestToApprove));
        }

        private async Task<Domain.Models.ApprovalRequests.ApprovalRequest> GetAprovalRequestAsync(ApproveMessagingRequestCommand request, CancellationToken cancellationToken)
        {
            return await _approvalRequestContext.ApprovalRequests.SingleOrDefaultAsync(x => x.ApprovalRequestId == request.ApprovalRequestId, cancellationToken);
        }
    }
}
