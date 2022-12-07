using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Commands.AddStudentToClass;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.Application.Validators.Section
{
    public class AddStudentToClassCommandValidator : AbstractValidator<AddStudentToClassCommand>
    {
        private readonly ISectionContext _sectionContext;
        private readonly IStudentContext _studentContext;

        public AddStudentToClassCommandValidator(ISectionContext sectionContext, IStudentContext studentContext)
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
                .Must(StudentNotAddedBeforeToThisClass).WithMessage(ResponseMessage.StudentAddedBeforToClass);
        }

        private bool StudentNotAddedBeforeToThisClass(AddStudentToClassCommand addStudentToClassCommand)
        {
            var section = _sectionContext.Sections
                                         .Include(i => i.Students)
                                         .FirstOrDefault(x => x.SectionId == addStudentToClassCommand.SectionId);

            if (section == null)
                throw new InvalidDataException(ResponseMessage.SectionNotFound);

            if (section.Students == null)
                return true;

            return addStudentToClassCommand!.StudentIds!.All(a => section.Students.All(d => d.StudentId != a));
        }

        private bool StudentExists(List<Guid> studentIdsToCheck)
        {
            return studentIdsToCheck!.Any(a => _studentContext.Students.Any(x => x.StudentId == a));
        }

        private bool SectionExists(Guid sectionIdToCheck)
        {
            return _sectionContext.Sections.Any(x => x.SectionId == sectionIdToCheck);
        }

    }
}
