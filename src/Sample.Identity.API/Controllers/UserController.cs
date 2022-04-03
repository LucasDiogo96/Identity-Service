using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.App.Contracts;
using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.Entities;

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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string id)
        {
            User? response = await service.Get(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            string userId = GetAuthenticatedUserId();

            User? response = await service.Get(userId);

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