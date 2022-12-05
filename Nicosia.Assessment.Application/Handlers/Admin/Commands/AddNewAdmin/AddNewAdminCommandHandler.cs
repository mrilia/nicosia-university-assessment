using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nicosia.Assessment.Application.Handlers.Admin.Dto;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.AddNewAdmin
{
    public class AddNewAdminCommandHandler : IRequestHandler<AddNewAdminCommand, Result<AdminDto>>
    {
        private readonly IAdminContext _context;
        private readonly IMapper _mapper;

        public AddNewAdminCommandHandler(IAdminContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<AdminDto>> Handle(AddNewAdminCommand request, CancellationToken cancellationToken)
        {
            var adminToAdd = _mapper.Map<Domain.Models.User.Admin>(request);

            await _context.Admins.AddAsync(adminToAdd, cancellationToken);
            await _context.SaveAsync(cancellationToken);

            return Result<AdminDto>.SuccessFul(_mapper.Map<AdminDto>(adminToAdd));
        }
    }
}
