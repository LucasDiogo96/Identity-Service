using System.Security.Claims;
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
        protected string GetAuthenticatedUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirst(e => e.Type == ClaimTypes.NameIdentifier).Value;
            }

            throw new System.UnauthorizedAccessException();
        }
    }
}