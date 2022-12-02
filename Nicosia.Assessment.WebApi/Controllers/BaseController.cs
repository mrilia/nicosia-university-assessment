using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Nicosia.Assessment.WebApi.Controllers
{
    [Route("[Controller]")]
    [EnableCors]
    [ApiController]
    public class BaseController : Controller
    {
    }
}
