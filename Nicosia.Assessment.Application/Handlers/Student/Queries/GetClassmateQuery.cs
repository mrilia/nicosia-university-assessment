using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Models;

namespace Nicosia.Assessment.Application.Handlers.Student.Queries
{
    public class GetClassmateQuery : PaginationRequest, IRequest<CollectionItems<ClassmateDto>>
    {
        public void SetStudentId(Guid studentId)
        {
            _studentId = studentId;
        }
        
        public Guid GetStudentId()
        {
            return _studentId;
        }

        private Guid _studentId { get; set; }

        public string Email { get; set; }
        public StudentSort Sort { get; set; }
    }

    public class GetClassmateQueryHandler : IRequestHandler<GetClassmateQuery, CollectionItems<ClassmateDto>>
    {
        private readonly ISectionContext _sectionContext;
        private readonly IMapper _mapper;

        public GetClassmateQueryHandler(ISectionContext context, IMapper mapper)
        {
            _sectionContext = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<ClassmateDto>> Handle(GetClassmateQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.User.Student> students = _sectionContext.Sections
                .Where(s => s.Students.Any(a => a.StudentId == request.GetStudentId()))
                !.SelectMany(s => s.Students)
                !.Where(i=> i.StudentId!= request.GetStudentId())
                !.Distinct();

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                students = students.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            students = ApplySort(students, request.Sort);
            var totalCount = await students.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<ClassmateDto>>(await students.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<ClassmateDto>(result, totalCount);
        }

        private static IQueryable<Domain.Models.User.Student> ApplySort(IQueryable<Domain.Models.User.Student> query, StudentSort sort = StudentSort.None)
        {
            query = sort switch
            {
                StudentSort.ByNameAsc => query.OrderBy(r => r.Lastname).ThenBy(r => r.Firstname),
                StudentSort.ByDateOfBirthAsc => query.OrderBy(r => r.DateOfBirth),
                _ => query
            };

            return query;
        }
    }
}
