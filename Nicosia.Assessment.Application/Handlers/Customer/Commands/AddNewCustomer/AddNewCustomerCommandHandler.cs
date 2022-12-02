
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Customer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Customer.Commands.AddNewCustomer
{
    public class AddNewCustomerCommandHandler : IRequestHandler<AddNewCustomerCommand, Result<CustomerDto>>
    {
        private readonly ICustomerContext _context;
        private readonly IMapper _mapper;

        public AddNewCustomerCommandHandler(ICustomerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CustomerDto>> Handle(AddNewCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToAdd = _mapper.Map<Domain.Models.Customer.Customer>(request);

            await _context.Customers.AddAsync(customerToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<CustomerDto>.SuccessFul(_mapper.Map<CustomerDto>(customerToAdd));
        }
    }
}
