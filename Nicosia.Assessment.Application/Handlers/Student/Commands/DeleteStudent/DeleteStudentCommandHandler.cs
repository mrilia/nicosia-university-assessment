﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result>
    {
        private readonly IStudentContext _context;

        public DeleteStudentCommandHandler(IStudentContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var customer = await GetCustomerAsync(request, cancellationToken);

            if (customer is null)
                return Result.Failed(new BadRequestObjectResult
                (new ApiMessage(ResponseMessage.StudentNotFound)));

            _context.Students.Remove(customer);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul();
        }

        private async Task<Domain.Models.User.Student> GetCustomerAsync(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            return await _context.Students.SingleOrDefaultAsync(x => x.StudentId == request.StudentId, cancellationToken);
        }

    }
}
