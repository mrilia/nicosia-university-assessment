using System.Linq;
using FluentValidation;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.UpdateAdmin;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Validators;

namespace Nicosia.Assessment.Application.Validators.Admin
{
    public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
    {
        private readonly IAdminContext _context;

        public UpdateAdminCommandValidator(IAdminContext context)
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
                .Must(AdminNotExists).WithMessage(ResponseMessage.AdminExists).WithErrorCode("201")
                .Must(EmailNotExists).WithMessage(ResponseMessage.EmailExists).WithErrorCode("202");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage(ResponseMessage.EmailIsRequired)
                .NotNull().WithMessage(ResponseMessage.EmailIsRequired)
                .Must(ValidEmailFormat).WithMessage(ResponseMessage.EmailNotValid).WithErrorCode("102");

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .Must(ValidPhoneNumberFormat).WithMessage(ResponseMessage.PhoneNumberNotValid).WithErrorCode("101");
        }

        private bool EmailNotExists(UpdateAdminCommand adminToCheck)
        {
            if (_context.Admins.Any(x => x.AdminId != adminToCheck.AdminId &&
                                                x.Email.Replace(" ", "").ToLower() == adminToCheck.Email.Replace(" ", "").ToLower()))
                return false;

            return true;
        }

        private bool ValidEmailFormat(string emailToCheck)
        {
            return EmailValidator.IsValid(emailToCheck);
        }

        private bool ValidPhoneNumberFormat(string phoneNumberToCheck)
        {
            return PhoneNumberValidator.IsValid(phoneNumberToCheck);
        }

        private bool AdminNotExists(UpdateAdminCommand adminToCheck)
        {
            if (_context.Admins.Any(x =>
                        x.AdminId != adminToCheck.AdminId &&
                        x.Firstname.Replace(" ", "").ToLower() == adminToCheck.Firstname.Replace(" ", "").ToLower() &&
                        x.Lastname.Replace(" ", "").ToLower() == adminToCheck.Lastname.Replace(" ", "").ToLower() &&
                        x.DateOfBirth == adminToCheck.DateOfBirth))
                return false;

            return true;
        }
    }
}
