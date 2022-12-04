using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.DeleteStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.UpdateStudent;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.WebApi.Controllers.Customer.V1
{
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new customer
        /// Sample Phone Number: 044 668 18 00
        /// Sample Bank Account Number: NL91ABNA0417164300
        /// </summary>
        /// <param name="addNewStudentCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create customer successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(StudentDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewStudentCommand addNewStudentCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewStudentCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetCustomerInfo", new { id = result.Data.Id }), result.Data);
        }



        /// <summary>
        /// Customer Info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Customer info</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="404">If customer not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(StudentDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetCustomerInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentQuery { Id = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Students 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Students list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<StudentDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string email, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetStudentListQuery { Email = email }, cancellationToken));




        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if delete successfully </response>
        /// <response code="404">If customer not found</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = id }, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }



        /// <summary>
        /// Update  Customer
        /// </summary>
        /// <param name="updateCustomerCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">if update customer successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="404">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentCommand updateCustomerCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateCustomerCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
