using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;

        public UpdateStudentCommandHandler(IStudentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var customerToUpdate = await GetCustomerAsync(request, cancellationToken);

            if (customerToUpdate is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.CustomerNotFound)));

            _mapper.Map(request, customerToUpdate);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.User.Student> GetCustomerAsync(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            return await _context.Students.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }

    }
}
