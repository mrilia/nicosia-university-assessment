using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Customer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Customer.Queries
{
    public class GetCustomerQuery : IRequest<Result<CustomerDto>>
    {
        public ulong Id { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Result<CustomerDto>>
    {
        private readonly ICustomerContext _context;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(ICustomerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (customer is null)
                return Result<CustomerDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.CustomerNotFound)));

            return Result<CustomerDto>.SuccessFul(_mapper.Map<CustomerDto>(customer));
        }
    }
}
