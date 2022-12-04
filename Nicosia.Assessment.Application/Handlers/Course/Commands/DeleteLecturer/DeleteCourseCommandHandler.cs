using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Course.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
    {
        private readonly ICourseContext _context;

        public DeleteCourseCommandHandler(ICourseContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var Course = await GetCourseAsync(request, cancellationToken);

            if (Course is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.CourseNotFound)));

            _context.Courses.Remove(Course);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.Course.Course> GetCourseAsync(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            return await _context.Courses.SingleOrDefaultAsync(x => x.CourseId == request.CourseId, cancellationToken);
        }

    }
}
