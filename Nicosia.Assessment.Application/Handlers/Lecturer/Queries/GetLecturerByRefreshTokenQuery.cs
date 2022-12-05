using System.Linq;
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
    public class GetLecturerByRefreshTokenQuery : IRequest<Result<LecturerDto>>
    {
        public string RefreshToken { get; set; }
    }

    public class GetLecturerByRefreshTokenHandler : IRequestHandler<GetLecturerByRefreshTokenQuery, Result<LecturerDto>>
    {
        private readonly ILecturerContext _context;
        private readonly IMapper _mapper;

        public GetLecturerByRefreshTokenHandler(ILecturerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<LecturerDto>> Handle(GetLecturerByRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var lecturer = await _context.Lecturers.SingleOrDefaultAsync(x => x.RefreshTokens.Any(s=>s.Token == request.RefreshToken),
                cancellationToken);

            if (lecturer is null)
                return Result<LecturerDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.LecturerNotFound)));

            return Result<LecturerDto>.SuccessFul(_mapper.Map<LecturerDto>(lecturer));
        }
    }
}
