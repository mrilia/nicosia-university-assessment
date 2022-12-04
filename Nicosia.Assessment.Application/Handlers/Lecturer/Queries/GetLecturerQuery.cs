using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Queries
{
    public class GetLecturerQuery : IRequest<Result<LecturerDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetLecturerQueryHandler : IRequestHandler<GetLecturerQuery, Result<LecturerDto>>
    {
        private readonly ILecturerContext _context;
        private readonly IMapper _mapper;

        public GetLecturerQueryHandler(ILecturerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<LecturerDto>> Handle(GetLecturerQuery request, CancellationToken cancellationToken)
        {
            var lecturer = await _context.Lecturers.SingleOrDefaultAsync(x => x.LecturerId == request.Id,
                cancellationToken);

            if (lecturer is null)
                return Result<LecturerDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.LecturerNotFound)));

            return Result<LecturerDto>.SuccessFul(_mapper.Map<LecturerDto>(lecturer));
        }
    }
}
