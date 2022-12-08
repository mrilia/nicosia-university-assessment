using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Options;
using Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Shared.Encryption;

namespace Nicosia.Assessment.Application.Validators.Student
{
    public class AuthenticateStudentCommandValidator : AbstractValidator<AuthenticateStudentCommand>
    {
        private readonly IStudentContext _context;

        public AuthenticateStudentCommandValidator(IStudentContext context)
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
                .Must(StudentExists).WithMessage(ResponseMessage.UsernamePasswordInvalid);
        }
        
        private bool StudentExists(AuthenticateStudentCommand studentToCheck)
        {
            var student = _context.Students.SingleOrDefault(x =>
                x.Email.ToLower().Trim() == studentToCheck.Username.ToLower().Trim());
            
            if (student == null || !new PasswordHasher(Options.Create<HashingOptions>(new HashingOptions())).Check(student.Password, studentToCheck.Password).Verified)
                return false;

            return true;
        }
    }
}
