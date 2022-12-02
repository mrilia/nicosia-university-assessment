using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nicosia.Assessment.Application.Handlers.Customer.Dto;
using Nicosia.Assessment.Application.Handlers.Customer.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;
using Xunit;

namespace Nicosia.Assessment.AcceptanceTests.Customer
{
    public class CustomerController
    {
        [Fact]
        public async Task WhenInvalidIdSend_ReturnNotFound()
        {
            var mockData = new Mock<IMediator>();

            mockData.Setup(x => x.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Result<CustomerDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.CustomerNotFound))));

            using var controller = new BaseConfiguration().WithMediatorService(mockData.Object).BuildCustomerController();

            var result = await controller.Get(It.IsAny<ulong>(), It.IsAny<CancellationToken>());

            var objectResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
    }
}
