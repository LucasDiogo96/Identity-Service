using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.App.Contracts;
using Sample.Identity.Domain.Commands;

namespace Sample.Identity.API.Controllers
{
    [AllowAnonymous]
    public class RegisterController : MainController
    {
        private readonly IUserService service;

        private readonly ILogger<UserController> logger;

        public RegisterController(IUserService service, ILogger<UserController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand model)
        {
            await service.Add(model);

            return Ok();
        }
    }
}