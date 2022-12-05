using System;
using MediatR;
using Nicosia.Assessment.Application.Results;

namespace Nicosia.Assessment.Application.Handlers.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommand : IRequest<Result>
    {
        public Guid AdminId { get; set; }
    }
}
