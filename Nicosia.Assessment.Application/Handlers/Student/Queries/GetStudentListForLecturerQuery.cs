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
    public class GetStudentListForLecturerQuery : PaginationRequest, IRequest<CollectionItems<StudentForLecturerDto>>
    {
        public void SetLecturerId(Guid lecturerId)
        {
            _lecturerId = lecturerId;
        }
        
        public Guid GetLecturerId()
        {
            return _lecturerId;
        }

        private Guid _lecturerId { get; set; }

        public string Email { get; set; }
        public StudentSort Sort { get; set; }
    }

    public class GetStudentListForLecturerQueryHandler : IRequestHandler<GetStudentListForLecturerQuery, CollectionItems<StudentForLecturerDto>>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;

        public GetStudentListForLecturerQueryHandler(IStudentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<StudentForLecturerDto>> Handle(GetStudentListForLecturerQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.User.Student> students = _context.Students.Where(i => i.Sections.Any(a => a.Lecturers.Any(l => l.LecturerId == request.GetLecturerId()))).Distinct();

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                students = students.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            students = ApplySort(students, request.Sort);
            var totalCount = await students.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<StudentForLecturerDto>>(await students.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<StudentForLecturerDto>(result, totalCount);
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
