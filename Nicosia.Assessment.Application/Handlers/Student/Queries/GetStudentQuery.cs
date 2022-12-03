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
    public class GetStudentQuery : IRequest<Result<StudentDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetStudentQuery, Result<StudentDto>>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IStudentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Students.SingleOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (customer is null)
                return Result<StudentDto>.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.CustomerNotFound)));

            return Result<StudentDto>.SuccessFul(_mapper.Map<StudentDto>(customer));
        }
    }
}
