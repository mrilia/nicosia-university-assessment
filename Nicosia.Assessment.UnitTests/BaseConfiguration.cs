using MediatR;
using Moq;
using Nicosia.Assessment.WebApi.Controllers.Customer.V1;

namespace Nicosia.Assessment.AcceptanceTests
{
    public class BaseConfiguration
    {
        private IMediator _mediator;

        public BaseConfiguration()
        {
            _mediator = new Mock<IMediator>().Object;
        }

        internal BaseConfiguration WithMediatorService(IMediator mediator)
        {
            _mediator = mediator;
            return this;
        }

        internal CustomerController BuildCustomerController() => new(_mediator);

    }
}
