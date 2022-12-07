using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Section.Commands.AddStudentToClass
{
    public class AddStudentToClassHandler : IRequestHandler<AddStudentToClassCommand, Result<ClassDto>>
    {
        private readonly ISectionContext _sectionContext;
        private readonly IStudentContext _studentContext;
        private readonly IMapper _mapper;

        public AddStudentToClassHandler(ISectionContext sectionContext, IStudentContext studentContext, IMapper mapper)
        {
            _sectionContext = sectionContext;
            _studentContext = studentContext;
            _mapper = mapper;
        }

        public async Task<Result<ClassDto>> Handle(AddStudentToClassCommand request, CancellationToken cancellationToken)
        {
            var sectionToUpdate = await GetSectionAsync(request, cancellationToken);

            foreach (var studentId in request.StudentIds!)
            {
                var studentToAdd = await GetStudentAsync(studentId, cancellationToken);
                sectionToUpdate.Students.Add(studentToAdd);
            }

            await _sectionContext.SaveAsync(cancellationToken);

            RemoveUselessChildrenFromResult(sectionToUpdate);

            return Result<ClassDto>.SuccessFul(_mapper.Map<ClassDto>(sectionToUpdate));
        }

        //todo: refctor to put this cod into auto mapper
        private void RemoveUselessChildrenFromResult(Domain.Models.Section.Section sectionToUpdate)
        {
            sectionToUpdate!.Course.Sections = null;
            sectionToUpdate!.Period.Sections = null;

            foreach (var student in sectionToUpdate.Students)
            {
                student!.Sections = null;
            }

            foreach (var lecturer in sectionToUpdate.Lecturers)
            {
                lecturer!.Sections = null;
            }
        }

        private async Task<Domain.Models.Section.Section> GetSectionAsync(AddStudentToClassCommand request, CancellationToken cancellationToken)
        {
            return await _sectionContext.Sections
                .Include(i=>i.Course)
                .Include(i=>i.Period)
                .Include(i=>i.Lecturers)
                .Include(i=>i.Students)
                .SingleOrDefaultAsync(x => x.SectionId == request.SectionId, cancellationToken);
        }

        private async Task<Domain.Models.User.Student> GetStudentAsync(Guid studentId, CancellationToken cancellationToken)
        {
            return await _studentContext.Students.SingleOrDefaultAsync(x => x.StudentId == studentId, cancellationToken);
        }
    }
}
