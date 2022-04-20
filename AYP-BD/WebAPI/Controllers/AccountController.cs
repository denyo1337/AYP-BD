using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Generate JWT for auth")]
        public async Task<IActionResult> Get([FromRoute] bool t)
        {
            return Ok();
        }
    }
}
