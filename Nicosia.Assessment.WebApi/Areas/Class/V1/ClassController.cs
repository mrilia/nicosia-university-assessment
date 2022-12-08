using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nicosia.Assessment.Application.Handlers.Lecturer.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Dto;
using Nicosia.Assessment.Application.Handlers.Section.Queries;
using Nicosia.Assessment.Application.Messages;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.WebApi.Controllers;
using Nicosia.Assessment.WebApi.Filters;

namespace Nicosia.Assessment.WebApi.Areas.Class.V1
{
    [Route("api/class/v1/[controller]")]
    public class ClassController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClassController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// List Of Classes 
        /// </summary>
        /// <param name="getClassListQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Class list</returns>
        /// <response code="200">if every thing is ok </response>
        /// <response code="400">If page or limit is overFlow</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PaginationResponse<ClassReportDto>), 200)]
        [ProducesResponseType(typeof(PaginationResponse<ClassReportWithStatisticsDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("all-classes-list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetClassList([FromQuery] GetClassListQuery getClassListQuery, CancellationToken cancellationToken)
        {
            var currentLecturer = HttpContext.Items["User"]! as LecturerDto;

            if (currentLecturer != null)
            {
                var classs = await _mediator.Send(_mapper.Map<GetAllClassListForLecturerQuery>(getClassListQuery), cancellationToken);
                var nextPageUrl = GetNextPageUrl(getClassListQuery, classs.TotalCount);
                var result = new PaginationResponse<ClassReportWithStatisticsDto>(classs.Items, classs.TotalCount, nextPageUrl);

                return Ok(result);
            }
            else
            {
                var classs = await _mediator.Send(_mapper.Map<GetAllClassListForStudentQuery>(getClassListQuery), cancellationToken);
                var nextPageUrl = GetNextPageUrl(getClassListQuery, classs.TotalCount);
                var result = new PaginationResponse<ChildlessClassDto>(classs.Items, classs.TotalCount, nextPageUrl);

                return Ok(result);
            }
        }
    }
}
