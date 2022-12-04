using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Interfaces;

namespace Nicosia.Assessment.Application.Handlers.Student.Queries
{
    public class GetStudentListQuery : IRequest<List<StudentDto>>
    {
        public string Email { get; set; }
    }


    public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQuery, List<StudentDto>>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;

        public GetStudentListQueryHandler(IStudentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StudentDto>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.User.Student> students = _context.Students;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                students = students.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            return _mapper.Map<List<StudentDto>>(await students.ToListAsync(cancellationToken));
        }
    }
}
