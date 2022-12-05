using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Encryption;

namespace Nicosia.Assessment.Application.Validators.Lecturer
{
    public class AuthenticateLecturerCommandValidator : AbstractValidator<AuthenticateLecturerCommand>
    {
        private readonly ILecturerContext _context;

        public AuthenticateLecturerCommandValidator(ILecturerContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Username)
                .NotEmpty().WithMessage(ResponseMessage.UsernameIsRequired).WithErrorCode("888")
                .NotNull().WithMessage(ResponseMessage.UsernameIsRequired).WithErrorCode("999");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage(ResponseMessage.PasswordIsRequired)
                .NotNull().WithMessage(ResponseMessage.PasswordIsRequired);
            
            RuleFor(dto => dto)
                .Must(LecturerExists).WithMessage(ResponseMessage.UsernamePasswordInvalid);
        }
        
        private bool LecturerExists(AuthenticateLecturerCommand lecturerToCheck)
        {
            var lecturer = _context.Lecturers.SingleOrDefault(x =>
                x.Email.ToLower().Trim() == lecturerToCheck.Username.ToLower().Trim());
            
            if (lecturer == null || !new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Check(lecturer.Password, lecturerToCheck.Password).Verified)
                return false;

            return true;
        }
    }
}
