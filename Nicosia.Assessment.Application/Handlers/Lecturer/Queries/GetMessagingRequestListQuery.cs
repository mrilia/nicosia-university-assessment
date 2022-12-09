using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Queries
{
    public class GetMessagingRequestListQuery : PaginationRequest, IRequest<CollectionItems<MessagingRequestDto>>
    {
        public void SetLecturerId(Guid LecturerId)
        {
            _lecturerId = LecturerId;
        }

        public Guid GetLecturerId()
        {
            return _lecturerId;
        }
        private Guid _lecturerId { get; set; }

        public string Section { get; set; }
        public ApprovalRequestStatus Status { get; set; }
        public MessagingRequestSort Sort { get; set; }

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessagingRequestSort
    {
        None,
        ByLastChangeTimeAsc,
        ByStatus,
        BySection
    }


    public class GetMessagingRequestListQueryHandler : IRequestHandler<GetMessagingRequestListQuery, CollectionItems<MessagingRequestDto>>
    {
        private readonly IApprovalRequestContext _context;
        private readonly IMapper _mapper;

        public GetMessagingRequestListQueryHandler(IApprovalRequestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CollectionItems<MessagingRequestDto>> Handle(GetMessagingRequestListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.ApprovalRequests.ApprovalRequest> messagingRequests = _context.ApprovalRequests
                .Include(i => i.Section)
                .Include(i => i.Lecturer)
                .Include(i => i.Student)
                .Where(i=>i.Section!.Lecturers!.Any(j=>j.LecturerId==request.GetLecturerId()));

            if (!string.IsNullOrWhiteSpace(request.Section))
            {
                messagingRequests = messagingRequests.Where(x => x.Section!.Number.ToLower().Contains(request.Section.ToLower()));
            }

            if (request.Status > 0)
            {
                messagingRequests = messagingRequests.Where(x => x.Status == request.Status);
            }

            messagingRequests = ApplySort(messagingRequests, request.Sort);
            var totalCount = await messagingRequests.LongCountAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<MessagingRequestDto>>(await messagingRequests.Skip(request.Offset).Take(request.Count).ToListAsync(cancellationToken));

            return new CollectionItems<MessagingRequestDto>(result, totalCount);
        }

        private static IQueryable<ApprovalRequest> ApplySort(IQueryable<ApprovalRequest> query, MessagingRequestSort sort = MessagingRequestSort.None)
        {
            query = sort switch
            {
                MessagingRequestSort.ByLastChangeTimeAsc => query.OrderBy(r => r.LastChange),
                MessagingRequestSort.BySection=> query.OrderBy(r => r.Section!.Number),
                MessagingRequestSort.ByStatus=> query.OrderBy(r => r.Status),
                _ => query
            };

            return query;
        }
    }
}
