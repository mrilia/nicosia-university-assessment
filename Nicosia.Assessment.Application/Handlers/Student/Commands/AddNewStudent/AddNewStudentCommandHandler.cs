using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent
{
    public class AddNewStudentCommandHandler : IRequestHandler<AddNewStudentCommand, Result<StudentDto>>
    {
        private readonly IStudentContext _context;
        private readonly IMapper _mapper;

        public AddNewStudentCommandHandler(IStudentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(AddNewStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToAdd = _mapper.Map<Domain.Models.User.Student>(request);

            await _context.Students.AddAsync(studentToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<StudentDto>.SuccessFul(_mapper.Map<StudentDto>(studentToAdd));
        }
    }
}
