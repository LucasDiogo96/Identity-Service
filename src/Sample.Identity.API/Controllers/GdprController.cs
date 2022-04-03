using Microsoft.AspNetCore.Mvc;

namespace Sample.Identity.API.Controllers
{
    public class GdprController : MainController
    {
        private readonly ILogger<GdprController> logger;

        public GdprController(ILogger<GdprController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Post([FromBody] object model)
        {
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}