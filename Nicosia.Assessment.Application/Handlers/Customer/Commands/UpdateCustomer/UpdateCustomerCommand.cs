using MediatR;
using Nicosia.Assessment.Application.Results;
using System;

namespace Nicosia.Assessment.Application.Handlers.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Result>
    {
        public ulong Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
