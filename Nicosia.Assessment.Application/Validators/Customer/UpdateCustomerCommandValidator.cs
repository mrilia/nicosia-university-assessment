using System.Linq;
using FluentValidation;
using Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Validators;

namespace Nicosia.Assessment.Application.Validators.Customer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        private readonly IStudentContext _context;

        public UpdateCustomerCommandValidator(IStudentContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Firstname)
                .NotEmpty().WithMessage(ResponseMessage.FirstnameIsRequired)
                .NotNull().WithMessage(ResponseMessage.FirstnameIsRequired);

            RuleFor(dto => dto.Lastname)
                .NotEmpty().WithMessage(ResponseMessage.LastnameIsRequired)
                .NotNull().WithMessage(ResponseMessage.LastnameIsRequired);

            RuleFor(dto => dto)
                .Must(CustomerNotExists).WithMessage(ResponseMessage.CustomerExists).WithErrorCode("201")
                .Must(EmailNotExists).WithMessage(ResponseMessage.EmailExists).WithErrorCode("202");

            RuleFor(dto => dto.DateOfBirth)
                .NotEmpty().WithMessage(ResponseMessage.DateOfBirthIsRequired)
                .NotNull().WithMessage(ResponseMessage.DateOfBirthIsRequired);

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage(ResponseMessage.EmailIsRequired)
                .NotNull().WithMessage(ResponseMessage.EmailIsRequired)
                .Must(ValidEmailFormat).WithMessage(ResponseMessage.EmailNotValid).WithErrorCode("102");

            RuleFor(dto => dto.BankAccountNumber)
                .NotEmpty().WithMessage(ResponseMessage.BankAccountNumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.BankAccountNumberIsRequired)
                .Must(ValidBankAccountFormat).WithMessage(ResponseMessage.BankAccountNumberNotValid).WithErrorCode("103");

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .Must(ValidPhoneNumberFormat).WithMessage(ResponseMessage.PhoneNumberNotValid).WithErrorCode("101");
        }

        private bool EmailNotExists(UpdateStudentCommand customerToCheck)
        {
            if (_context.Students.Any(x => x.Id != customerToCheck.Id &&
                                                x.Email.Replace(" ", "").ToLower() == customerToCheck.Email.Replace(" ", "").ToLower()))
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

        private bool CustomerNotExists(UpdateStudentCommand customerToCheck)
        {
            if (_context.Students.Any(x =>
                        x.Id != customerToCheck.Id &&
                        x.Firstname.Replace(" ", "").ToLower() == customerToCheck.Firstname.Replace(" ", "").ToLower() &&
                        x.Lastname.Replace(" ", "").ToLower() == customerToCheck.Lastname.Replace(" ", "").ToLower()))
                return false;

            return true;
        }
    }
}
