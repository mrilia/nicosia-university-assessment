using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Queries
{
    public class GetStudentByIdQuery : IRequest<Result<StudentDto>>
    {
        public Guid StudentId { get; set; }
    }

    public class GetStudentQueryHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;

        public GetStudentQueryHandler(IStudentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x => x.StudentId == request.StudentId,
                cancellationToken);

            if (student is null)
                return Result<StudentDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.StudentNotFound)));

            return Result<StudentDto>.SuccessFul(_mapper.Map<StudentDto>(student));
        }
    }
}
