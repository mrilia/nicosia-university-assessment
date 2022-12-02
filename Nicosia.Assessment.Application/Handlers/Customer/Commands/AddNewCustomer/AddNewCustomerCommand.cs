using MediatR;
using Nicosia.Assessment.Application.Handlers.Customer.Dto;
using System;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Customer.Commands.AddNewCustomer
{
    public class AddNewCustomerCommand : IRequest<Result<CustomerDto>>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
