using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Transfers.Recovery;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.API.Controllers
{
    [AllowAnonymous]
    public class RecoveryController : MainController
    {
        private readonly IRecoveryService service;
        private readonly ILogger<RecoveryController> logger;

        public RecoveryController(IRecoveryService service, ILogger<RecoveryController> logger)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] PasswordRecoveryRequestTransfer model)
        {
            service.SendRecoveryCode(model);

            return Ok();
        }

        [HttpPost("Confirm")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RecoveryCode), StatusCodes.Status200OK)]
        public IActionResult Confirm([FromBody] PasswordRecoveryConfirmTransfer model)
        {
            RecoveryCode response = service.ConfirmRecoveryCode(model);

            return Ok(response);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody] PasswordRecoveryTransfer model)
        {
            bool response = service.ChangePassword(model);

            return Ok(response);
        }
    }
}