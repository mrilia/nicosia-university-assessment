using System.Linq;
using FluentValidation;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.AddNewLecturer;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Validators;

namespace Nicosia.Assessment.Application.Validators.Lecturer
{
    public class AddNewLecturerCommandValidator : AbstractValidator<AddNewLecturerCommand>
    {
        private readonly ILecturerContext _context;

        public AddNewLecturerCommandValidator(ILecturerContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Firstname)
                .NotEmpty().WithMessage(ResponseMessage.FirstnameIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.FirstnameIsRequired).WithErrorCode("999");

            RuleFor(dto => dto.Lastname)
                .NotEmpty().WithMessage(ResponseMessage.LastnameIsRequired)
                .NotNull().WithMessage(ResponseMessage.LastnameIsRequired);
            
            RuleFor(dto => dto)
                .Must(LecturerNotExists).WithMessage(ResponseMessage.LecturerExists).WithErrorCode("201");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage(ResponseMessage.EmailIsRequired)
                .NotNull().WithMessage(ResponseMessage.EmailIsRequired)
                .Must(ValidEmailFormat).WithMessage(ResponseMessage.EmailNotValid).WithErrorCode("102")
                .Must(EmailNotExists).WithMessage(ResponseMessage.EmailExists).WithErrorCode("202");
            
            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .NotNull().WithMessage(ResponseMessage.PhoneNumberIsRequired)
                .Must(ValidPhoneNumberFormat).WithMessage(ResponseMessage.PhoneNumberNotValid).WithErrorCode("101");
        }

        private bool EmailNotExists(string emailToCheck)
        {
            if (_context.Lecturers.Any(x => x.Email.Replace(" ", "").ToLower() == emailToCheck.Replace(" ", "").ToLower()))
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

        private bool LecturerNotExists(AddNewLecturerCommand lecturerToCheck)
        {
            if (_context.Lecturers.Any(x =>
                        x.Firstname.Replace(" ", "").ToLower() == lecturerToCheck.Firstname.Replace(" ", "").ToLower() &&
                        x.Lastname.Replace(" ", "").ToLower() == lecturerToCheck.Lastname.Replace(" ", "").ToLower() &&
                        x.DateOfBirth == lecturerToCheck.DateOfBirth))
                return false;

            return true;
        }
    }
}
