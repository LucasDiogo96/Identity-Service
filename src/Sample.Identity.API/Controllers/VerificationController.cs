using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.App.Contracts;
using Sample.Identity.Domain.Enumerators;

namespace Sample.Identity.API.Controllers
{
    [Authorize]
    public class VerificationController : MainController
    {
        private readonly IIdentityService service;

        public VerificationController(IIdentityService service)
        {
            this.service = service;
        }

        [HttpGet("{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Notify(NotificationType type)
        {
            string userId = GetAuthenticatedUserId();

            await service.VerifyIdentity(userId, type);

            return Ok();
        }

        [HttpGet("email/{code}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyEmail(string code)
        {
            string userId = GetAuthenticatedUserId();

            bool response = await service.VerifyEmail(userId, code);

            return Ok(response);
        }

        [HttpGet("phone/{code}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyPhone(string code)
        {
            string userId = GetAuthenticatedUserId();

            bool response = await service.VerifyPhone(userId, code);

            return Ok(response);
        }
    }
}