using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Nicosia.Assessment.Application.Models;
using Nicosia.Assessment.Application.Handlers.Student.Dto;
using Nicosia.Assessment.Application.Handlers.Student.Queries;
using Nicosia.Assessment.Application.Messages;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace Nicosia.Assessment.WebApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class HealthCheckController : Controller
    {

        /// <summary>
        /// Health Check 
        /// </summary>
        /// <returns> String</returns>
        /// <response code="500">If an unexpected error happen</response>
        [AllowAnonymous]
        [HttpGet("health-check")]
        public async Task<IActionResult> Get()
        {
            return Ok("Nicosia Assessment Web Api is UP...");
        }

    }
}
