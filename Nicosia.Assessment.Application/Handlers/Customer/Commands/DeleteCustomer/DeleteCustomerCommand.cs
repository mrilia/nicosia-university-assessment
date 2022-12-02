using MediatR;
using Nicosia.Assessment.Application.Results;


namespace Nicosia.Assessment.Application.Handlers.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
    }
}
