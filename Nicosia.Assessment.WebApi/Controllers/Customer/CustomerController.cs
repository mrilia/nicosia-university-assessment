using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Customer.Commands.AddNewCustomer;
using Nicosia.Assessment.Application.Handlers.Customer.Commands.DeleteCustomer;
using Nicosia.Assessment.Application.Handlers.Customer.Commands.UpdateCustomer;
using Nicosia.Assessment.Application.Handlers.Customer.Dto;
using Nicosia.Assessment.Application.Handlers.Customer.Queries;
using Nicosia.Assessment.Application.Messages;

namespace Nicosia.Assessment.WebApi.Controllers.Customer
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
        /// <param name="addNewCustomerCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">if create customer successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(CustomerDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddNew(AddNewCustomerCommand addNewCustomerCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(addNewCustomerCommand, cancellationToken);

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
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetCustomerInfo")]
        public async Task<IActionResult> Get(ulong id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCustomerQuery { Id = id }, cancellationToken);

            return result.ApiResult;
        }



        /// <summary>
        /// List Of Customers 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Customers list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<CustomerDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string email, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetCustomersListQuery { Email = email }, cancellationToken));




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
        public async Task<IActionResult> Delete(ulong id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { Id = id }, cancellationToken);

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
        public async Task<IActionResult> Update(UpdateCustomerCommand updateCustomerCommand,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateCustomerCommand, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
