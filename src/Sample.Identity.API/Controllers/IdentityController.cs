using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Transfers;
using Sample.Identity.Infra.Models;
using Microsoft.AspNetCore.HttpOverrides;

namespace Sample.Identity.API.Controllers
{
    [AllowAnonymous]
    public class IdentityController : MainController
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IIdentityService service;

        public IdentityController(IIdentityService service, IHttpContextAccessor accessor)
        {
            this.service = service;
            this.accessor = accessor;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserIdentity), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] IdentitySignInTransfer model)
        {
            model.RemoteAddress = accessor.HttpContext.Connection.RemoteIpAddress?.ToString();

            UserIdentity? response = service.SignIn(model);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserIdentity), StatusCodes.Status200OK)]
        public IActionResult Put([FromBody] IdentityRefreshTransfer model)
        {
            UserIdentity? response = service.Refresh(model);

            return Ok(response);
        }
    }
}