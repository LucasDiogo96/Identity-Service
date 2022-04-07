using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.API.Filters;

namespace Sample.Identity.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ModelStateValidation]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class MainController : ControllerBase
    {
        protected string GetAuthenticatedUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirst(e => e.Type == ClaimTypes.NameIdentifier).Value;
            }

            throw new UnauthorizedAccessException();
        }
    }
}