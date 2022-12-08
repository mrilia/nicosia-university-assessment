using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Application.Validators.Lecturer;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.RejectMessagingRequest
{
    public class RejectMessagingRequestCommandHandler : IRequestHandler<RejectMessagingRequestCommand, Result<RejectRequestDto>>
    {
        private readonly IApprovalRequestContext _approvalRequestContext;
        private readonly ILecturerContext _lecturerContext;
        private readonly ISectionContext _sectionContext;
        private readonly IMapper _mapper;

        public RejectMessagingRequestCommandHandler(IApprovalRequestContext approvalRequestContext,
            ILecturerContext lecturerContext,
            ISectionContext sectionContext,
            IMapper mapper)
        {
            _approvalRequestContext = approvalRequestContext;
            _lecturerContext = lecturerContext;
            _sectionContext = sectionContext;
            _mapper = mapper;
        }

        public async Task<Result<RejectRequestDto>> Handle(RejectMessagingRequestCommand request, CancellationToken cancellationToken)
        {
            RejectMessagingRequestCommandValidator customerValidator = new RejectMessagingRequestCommandValidator(_lecturerContext, _sectionContext, _approvalRequestContext);
            var validatorResult = customerValidator.Validate(request);

            if (!validatorResult.IsValid)
                return Result<RejectRequestDto>.Failed(new BadRequestObjectResult
                    (new ApiMessage(validatorResult.Errors.First().ErrorMessage)));

            var approvalRequestToReject = await GetAprovalRequestAsync(request, cancellationToken);

            approvalRequestToReject.Status = ApprovalRequestStatus.Rejected;
            approvalRequestToReject.LecturerId = request.LecturerId;
            approvalRequestToReject.LastChange = DateTime.Now;

            await _approvalRequestContext.SaveAsync(cancellationToken);

            return Result<RejectRequestDto>.SuccessFul(_mapper.Map<RejectRequestDto>(approvalRequestToReject));
        }

        private async Task<Domain.Models.ApprovalRequests.ApprovalRequest> GetAprovalRequestAsync(RejectMessagingRequestCommand request, CancellationToken cancellationToken)
        {
            return await _approvalRequestContext.ApprovalRequests.SingleOrDefaultAsync(x => x.ApprovalRequestId == request.ApprovalRequestId, cancellationToken);
        }
    }
}
