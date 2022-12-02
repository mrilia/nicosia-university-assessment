using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Customer.Commands.AddNewCustomer;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Validators;
using System.Linq;

namespace Nicosia.Assessment.Application.Validators.Customer
{
    public class AddNewCustomerCommandValidator : AbstractValidator<AddNewCustomerCommand>
    {
        private readonly ICustomerContext _context;

        public AddNewCustomerCommandValidator(ICustomerContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Firstname)
                .NotEmpty().WithMessage(ResponseMessage.FirstnameIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.FirstnameIsRequired).WithErrorCode("999");

            RuleFor(dto => dto.Lastname)
                .NotEmpty().WithMessage(ResponseMessage.LastnameIsRequired)
                .NotNull().WithMessage(ResponseMessage.LastnameIsRequired);

            RuleFor(dto => dto.DateOfBirth)
                .NotEmpty().WithMessage(ResponseMessage.DateOfBirthIsRequired)
                .NotNull().WithMessage(ResponseMessage.DateOfBirthIsRequired);

            RuleFor(dto => dto)
                .Must(CustomerNotExists).WithMessage(ResponseMessage.CustomerExists).WithErrorCode("201");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage(ResponseMessage.EmailIsRequired)
                .NotNull().WithMessage(ResponseMessage.EmailIsRequired)
                .Must(ValidEmailFormat).WithMessage(ResponseMessage.EmailNotValid).WithErrorCode("102")
                .Must(EmailNotExists).WithMessage(ResponseMessage.EmailExists).WithErrorCode("202");

            RuleFor(dto => dto.BankAccountNumber)
                .NotEmpty().WithMessage(ResponseMessage.BankAccountNumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.BankAccountNumberIsRequired)
                .Must(ValidBankAccountFormat).WithMessage(ResponseMessage.BankAccountNumberNotValid).WithErrorCode("103");

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .Must(ValidPhoneNumberFormat).WithMessage(ResponseMessage.PhoneNumberNotValid).WithErrorCode("101");
        }

        private bool EmailNotExists(string emailToCheck)
        {
            if (_context.Customers.Any(x => x.Email.Replace(" ", "").ToLower() == emailToCheck.Replace(" ", "").ToLower()))
                return false;

            return true;
        }

        private bool ValidEmailFormat(string emailToCheck)
        {
            return EmailValidator.IsValid(emailToCheck);
        }

        private bool ValidBankAccountFormat(string bankAccountToCheck)
        {
            return BankAccountValidator.IsValid(bankAccountToCheck);
        }

        private bool ValidPhoneNumberFormat(string phoneNumberToCheck)
        {
            return PhoneNumberValidator.IsValid(phoneNumberToCheck);
        }

        private bool CustomerNotExists(AddNewCustomerCommand customerToCheck)
        {
            if (_context.Customers.Any(x =>
                        x.Firstname.Replace(" ", "").ToLower() == customerToCheck.Firstname.Replace(" ", "").ToLower() &&
                        x.Lastname.Replace(" ", "").ToLower() == customerToCheck.Lastname.Replace(" ", "").ToLower() &&
                        x.DateOfBirth == customerToCheck.DateOfBirth))
                return false;

            return true;
        }
    }
}
