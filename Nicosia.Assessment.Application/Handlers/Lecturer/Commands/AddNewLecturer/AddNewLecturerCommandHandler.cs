using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Lecturer.Commands.AddNewLecturer
{
    public class AddNewLecturerCommandHandler : IRequestHandler<AddNewLecturerCommand, Result<LecturerDto>>
    {
        private readonly ILecturerContext _context;
        private readonly IMapper _mapper;

        public AddNewLecturerCommandHandler(ILecturerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<LecturerDto>> Handle(AddNewLecturerCommand request, CancellationToken cancellationToken)
        {
            var lecturerToAdd = _mapper.Map<Domain.Models.User.Lecturer>(request);

            await _context.Lecturers.AddAsync(lecturerToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<LecturerDto>.SuccessFul(_mapper.Map<LecturerDto>(lecturerToAdd));
        }
    }
}
