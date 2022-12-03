//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Nicosia.Assessment.WebApi.Controllers.Authenticate.V1.Models;
//using Nicosia.Assessment.WebApi.Models;
//using SuperDelivery.Attendance.Application.Users;

//namespace SuperDelivery.Attendance.Api.Admin.Authenticate.V1;

//[ApiController]
//[ApiVersion("1.0")]
//[Route("admin-api/authenticate")]
//[Route("admin-api/v{version:apiVersion}/authenticate")]
//public class AuthenticateController : ControllerBase
//{
//    private readonly IUserService _userService;

//    public AuthenticateController(IUserService userService)
//    {
//        _userService = userService;
//    }

//    [HttpPost]
//    [AllowAnonymous]
//    [ProducesResponseType(typeof(Response<AuthenticateResponse>), StatusCodes.Status200OK)]
//    public async Task<ActionResult<Response<AuthenticateResponse>>> Authenticate(AuthenticateRequest request)
//    {
//        var authenticatedUserDto = await _userService.AdminAuthenticateAsync(request.Email,  request.Password!);
//        var result = new Response<AuthenticateResponse>(AuthenticateResponse.Build(authenticatedUserDto));

//        return Ok(result);
//    }
//}