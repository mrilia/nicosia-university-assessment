using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Models;

namespace Nicosia.Assessment.Application.Handlers.Admin.Queries
{
    public class GetAdminListQuery : PaginationRequest, IRequest<CollectionItems<AdminDto>>
    {
        public string Email { get; set; }
        public AdminSort Sort { get; set; }

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AdminSort
    {
        None,
        ByNameAsc,
        ByDateOfBirthAsc
    }


    public class GetAdminListQueryHandler : IRequestHandler<GetAdminListQuery, CollectionItems<AdminDto>>
    {
        private readonly IAdminContext _context;
        private readonly IMapper _mapper;

        public GetAdminListQueryHandler(IAdminContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<AdminDto>> Handle(GetAdminListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.User.Admin> admins = _context.Admins;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                admins = admins.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            admins = ApplySort(admins, request.Sort);
            var totalCount = await admins.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<AdminDto>>(await admins.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<AdminDto>(result, totalCount);
        }

        private static IQueryable<Domain.Models.User.Admin> ApplySort(IQueryable<Domain.Models.User.Admin> query, AdminSort sort = AdminSort.None)
        {
            query = sort switch
            {
                AdminSort.ByNameAsc => query.OrderBy(r => r.Lastname).ThenBy(r => r.Firstname),
                AdminSort.ByDateOfBirthAsc => query.OrderBy(r => r.DateOfBirth),
                _ => query
            };

            return query;
        }
    }
}
