using MediatR;
using Moq;
using Nicosia.Assessment.WebApi.Controllers.Student.V1;

namespace Nicosia.Assessment.AcceptanceTests
{
    public class BaseConfiguration
    {
        private IMediator _mediator;

        public BaseConfiguration()
        {
            _mediator = new Mock<IMediator>().Object;
        }

        public BaseConfiguration WithMediatorService(IMediator mediator)
        {
            _mediator = mediator;
            return this;
        }

        public StudentController BuildStudentController() => new(_mediator);

    }
}
