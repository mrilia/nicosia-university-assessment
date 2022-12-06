using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Models;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Queries
{
    public class GetLecturerListQuery : PaginationRequest, IRequest<CollectionItems<LecturerDto>>
    {
        public string Email { get; set; }
        public LecturerSort Sort { get; set; }

    }
    public enum LecturerSort
    {
        None,
        ByNameAsc,
        ByDateOfBirthAsc
    }


    public class GetLecturerListQueryHandler : IRequestHandler<GetLecturerListQuery, CollectionItems<LecturerDto>>
    {
        private readonly ILecturerContext _context;
        private readonly IMapper _mapper;

        public GetLecturerListQueryHandler(ILecturerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<LecturerDto>> Handle(GetLecturerListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.User.Lecturer> lecturers = _context.Lecturers;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                lecturers = lecturers.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            lecturers = ApplySort(lecturers, request.Sort);
            var totalCount = await lecturers.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<LecturerDto>>(await lecturers.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<LecturerDto>(result, totalCount);
        }

        private static IQueryable<Domain.Models.User.Lecturer> ApplySort(IQueryable<Domain.Models.User.Lecturer> query, LecturerSort sort = LecturerSort.None)
        {
            query = sort switch
            {
                LecturerSort.ByNameAsc => query.OrderBy(r => r.Lastname).ThenBy(r => r.Firstname),
                LecturerSort.ByDateOfBirthAsc => query.OrderBy(r => r.DateOfBirth),
                _ => query
            };

            return query;
        }
    }
}
