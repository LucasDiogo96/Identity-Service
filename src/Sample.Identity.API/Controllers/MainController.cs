using Microsoft.AspNetCore.Mvc;
using Sample.Identity.API.Filters;

namespace Sample.Identity.API.Controllers
{
    [ApiController]
    [ModelStateValidation]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MainController : ControllerBase
    {
    }
}