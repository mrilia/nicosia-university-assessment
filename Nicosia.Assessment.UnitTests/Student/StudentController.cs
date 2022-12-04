using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Results;
using Xunit;

namespace Nicosia.Assessment.AcceptanceTests.Student
{
    public class StudentController
    {
        [Fact]
        public async Task WhenInvalidIdSend_ReturnNotFound()
        {
            var mockData = new Mock<IMediator>();

            mockData.Setup(x => x.Send(It.IsAny<GetStudentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    Result<StudentDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.StudentNotFound))));

            using var controller = new BaseConfiguration().WithMediatorService(mockData.Object).BuildStudentController();

            var result = await controller.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            var objectResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
    }
}
