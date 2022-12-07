using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Commands.WithdrawStudentToClass;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Section
{
    public class WithdrawStudentFromClassCommandValidator : AbstractValidator<WithdrawStudentFromClassCommand>
    {
        private readonly ISectionContext _sectionContext;
        private readonly IStudentContext _studentContext;

        public WithdrawStudentFromClassCommandValidator(ISectionContext sectionContext, IStudentContext studentContext)
        {
            _sectionContext = sectionContext;
            _studentContext = studentContext;

            //CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.SectionId)
                .NotEmpty().WithMessage(ResponseMessage.SectionIdIsRequired)
                .NotNull().WithMessage(ResponseMessage.SectionIdIsRequired)
                .Must(SectionExists).WithMessage(ResponseMessage.SectionNotFound);

            RuleFor(dto => dto.StudentIds)
                .NotNull().WithMessage(ResponseMessage.StudentIdIsRequired)
                .NotEmpty().WithMessage(ResponseMessage.StudentIdIsRequired)
                .Must(StudentExists).WithMessage(ResponseMessage.StudentNotFound);

            RuleFor(dto => dto)
                .Must(StudentAddedBeforeToThisClass).WithMessage(ResponseMessage.StudentNotAddedBeforeToClass);
        }

        private bool StudentAddedBeforeToThisClass(WithdrawStudentFromClassCommand withdrawStudentFromClassCommand)
        {
            var section = _sectionContext.Sections
                .Include(i => i.Students)
                .FirstOrDefault(x => x.SectionId == withdrawStudentFromClassCommand.SectionId);

            if (section == null)
                throw new InvalidDataException(ResponseMessage.SectionNotFound);

            if (section.Students == null)
                return true;

            return withdrawStudentFromClassCommand!.StudentIds!.All(a => section.Students.Any(d => d.StudentId == a));
        }

        private bool StudentExists(List<Guid> studentIdToCheck)
        {
            return studentIdToCheck?.Any(a => _studentContext.Students.Any(x => x.StudentId == a)) ?? false;
        }

        private bool SectionExists(Guid sectionIdToCheck)
        {
            return _sectionContext.Sections.Any(x => x.SectionId == sectionIdToCheck);
        }

    }
}
