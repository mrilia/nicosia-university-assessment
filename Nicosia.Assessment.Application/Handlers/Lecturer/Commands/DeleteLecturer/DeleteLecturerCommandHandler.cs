using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.DeleteLecturer
{
    public class DeleteLecturerCommandHandler : IRequestHandler<DeleteLecturerCommand, Result>
    {
        private readonly ILecturerContext _context;

        public DeleteLecturerCommandHandler(ILecturerContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteLecturerCommand request, CancellationToken cancellationToken)
        {
            var lecturer = await GetLecturerAsync(request, cancellationToken);

            if (lecturer is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.LecturerNotFound)));

            _context.Lecturers.Remove(lecturer);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.User.Lecturer> GetLecturerAsync(DeleteLecturerCommand request, CancellationToken cancellationToken)
        {
            return await _context.Lecturers.SingleOrDefaultAsync(x => x.LecturerId == request.LecturerId, cancellationToken);
        }

    }
}
