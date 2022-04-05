using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Transfers.User;
using Sample.Identity.Domain.Commands;

namespace Sample.Identity.API.Controllers
{
    [Authorize]
    public class UserController : MainController
    {
        private readonly IUserService service;

        private readonly ILogger<UserController> logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponseTransfer), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            string userId = GetAuthenticatedUserId();

            UserResponseTransfer? response = await service.Get(userId);

            return Ok(response);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Patch([FromBody] UpdateUserCommand model)
        {
            service.Update(model);

            return Ok();
        }
    }
}