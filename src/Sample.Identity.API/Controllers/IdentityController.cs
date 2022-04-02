using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Transfers;
using Sample.Identity.Infra.Models;

namespace Sample.Identity.API.Controllers
{
    [AllowAnonymous]
    public class IdentityController : MainController
    {
        private readonly IIdentityService service;

        public IdentityController(IIdentityService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserIdentity), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] IdentitySignInTransfer model)
        {
            UserIdentity? response = await service.SignIn(model);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserIdentity), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] IdentityRefreshTransfer model)
        {
            UserIdentity? response = await service.Refresh(model);

            return Ok(response);
        }
    }
}