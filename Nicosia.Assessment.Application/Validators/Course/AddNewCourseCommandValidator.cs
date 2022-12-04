using System.Linq;
using FluentValidation;
using Nicosia.Assessment.Application.Handlers.Course.Commands.AddNewCourse;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Course
{
    public class AddNewCourseCommandValidator : AbstractValidator<AddNewCourseCommand>
    {
        private readonly ICourseContext _context;

        public AddNewCourseCommandValidator(ICourseContext context)
        {
            _context = context;
            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Code)
                .NotEmpty().WithMessage(ResponseMessage.CodeIsRequired)
                .NotNull().WithMessage(ResponseMessage.CodeIsRequired);

            RuleFor(dto => dto)
                .Must(CodeNotExists).WithMessage(ResponseMessage.CourseExists).WithErrorCode("202");

            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage(ResponseMessage.TitleIsRequired)
                .NotNull().WithMessage(ResponseMessage.TitleIsRequired);
        }

        private bool CodeNotExists(AddNewCourseCommand courseToCheck)
        {
            if (_context.Courses.Any(x => x.Code.Replace(" ", "").ToLower() == courseToCheck.Code.Replace(" ", "").ToLower()))
                return false;

            return true;
        }
    }
}
