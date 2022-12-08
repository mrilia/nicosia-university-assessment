using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;
using Nicosia.Assessment.Application.Validators.Student;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewMessagingRequest
{
    public class AddNewSMessagingRequestCommandHandler : IRequestHandler<AddNewMessagingRequestCommand, Result<ApprovalRequestDto>>
    {
        private readonly IApprovalRequestContext _approvalRequestContext;
        private readonly IStudentContext _studentContext;
        private readonly ISectionContext _sectionContext;
        private readonly IMapper _mapper;

        public AddNewSMessagingRequestCommandHandler(IApprovalRequestContext approvalRequestContext,
            IStudentContext studentContext,
            ISectionContext sectionContext,
            IMapper mapper)
        {
            _approvalRequestContext = approvalRequestContext;
            _studentContext = studentContext;
            _sectionContext = sectionContext;
            _mapper = mapper;
        }

        public async Task<Result<ApprovalRequestDto>> Handle(AddNewMessagingRequestCommand request, CancellationToken cancellationToken)
        {
            AddNewSMessagingRequestCommandValidator customerValidator = new AddNewSMessagingRequestCommandValidator(_studentContext, _sectionContext, _approvalRequestContext);
            var validatorResult = customerValidator.Validate(request);
            
            if (!validatorResult.IsValid)
                return Result<ApprovalRequestDto>.Failed(new BadRequestObjectResult
                    (new ApiMessage(validatorResult.Errors.First().ErrorMessage)));

            var requestToAdd = _mapper.Map<ApprovalRequest>(request);

            requestToAdd.Status = ApprovalRequestStatus.Waiting;

            await _approvalRequestContext.ApprovalRequests.AddAsync(requestToAdd, cancellationToken);
            await _approvalRequestContext.SaveAsync(cancellationToken);

            return Result<ApprovalRequestDto>.SuccessFul(_mapper.Map<ApprovalRequestDto>(requestToAdd));
        }
    }
}
