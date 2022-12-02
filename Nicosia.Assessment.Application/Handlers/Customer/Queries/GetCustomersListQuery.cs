using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Application.Handlers.Customer.Dto;
using Nicosia.Assessment.Application.Interfaces;

namespace Nicosia.Assessment.Application.Handlers.Customer.Queries
{
    public class GetCustomersListQuery : IRequest<List<CustomerDto>>
    {
        public string Email { get; set; }
    }


    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomersListQuery, List<CustomerDto>>
    {
        private readonly ICustomerContext _context;
        private readonly IMapper _mapper;

        public GetCustomerListQueryHandler(ICustomerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Models.Customer.Customer> customers = _context.Customers;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                customers = customers.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            return _mapper.Map<List<CustomerDto>>(await customers.ToListAsync(cancellationToken));
        }
    }
}
