using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Queries
{
    public class GetLecturerListQuery : IRequest<List<LecturerDto>>
    {
        public string Email { get; set; }
    }


    public class GetLecturerListQueryHandler : IRequestHandler<GetLecturerListQuery, List<LecturerDto>>
    {
        private readonly ILecturerContext _context;
        private readonly IMapper _mapper;

        public GetLecturerListQueryHandler(ILecturerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LecturerDto>> Handle(GetLecturerListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.User.Lecturer> lecturers = _context.Lecturers;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                lecturers = lecturers.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            return _mapper.Map<List<LecturerDto>>(await lecturers.ToListAsync(cancellationToken));
        }
    }
}
