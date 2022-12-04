using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.UpdateLecturer
{
    public class UpdateLecturerCommandHandler : IRequestHandler<UpdateLecturerCommand, Result>
    {
        private readonly ILecturerContext _context;
        private readonly IMapper _mapper;

        public UpdateLecturerCommandHandler(ILecturerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateLecturerCommand request, CancellationToken cancellationToken)
        {
            var lecturerToUpdate = await GetLecturerAsync(request, cancellationToken);

            if (lecturerToUpdate is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.LecturerNotFound)));

            _mapper.Map(request, lecturerToUpdate);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.User.Lecturer> GetLecturerAsync(UpdateLecturerCommand request, CancellationToken cancellationToken)
        {
            return await _context.Lecturers.SingleOrDefaultAsync(x => x.LecturerId == request.LecturerId, cancellationToken);
        }

    }
}
