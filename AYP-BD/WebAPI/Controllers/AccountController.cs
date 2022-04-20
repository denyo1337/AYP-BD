using Application.Functions.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("ayb/api/[controller]")]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("test/{text}")]
        [SwaggerOperation(Summary = "Test")]
        public async Task<IActionResult> Get([FromRoute] string text)
        {
            var test = await _mediator.Send(new TestCommand(text));
            return Ok(test);
        }
    }
}
