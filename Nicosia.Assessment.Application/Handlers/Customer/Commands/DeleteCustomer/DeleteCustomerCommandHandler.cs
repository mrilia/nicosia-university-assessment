using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
    {
        private readonly ICustomerContext _context;

        public DeleteCustomerCommandHandler(ICustomerContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await GetCustomerAsync(request, cancellationToken);

            if (customer is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.CustomerNotFound)));

            _context.Customers.Remove(customer);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.Customer.Customer> GetCustomerAsync(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }

    }
}
