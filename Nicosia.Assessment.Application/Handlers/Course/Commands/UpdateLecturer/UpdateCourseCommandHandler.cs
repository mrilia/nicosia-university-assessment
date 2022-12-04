using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Course.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result>
    {
        private readonly ICourseContext _context;
        private readonly IMapper _mapper;

        public UpdateCourseCommandHandler(ICourseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var CourseToUpdate = await GetCourseAsync(request, cancellationToken);

            if (CourseToUpdate is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.CourseNotFound)));

            _mapper.Map(request, CourseToUpdate);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.Course.Course> GetCourseAsync(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            return await _context.Courses.SingleOrDefaultAsync(x => x.CourseId == request.CourseId, cancellationToken);
        }

    }
}
