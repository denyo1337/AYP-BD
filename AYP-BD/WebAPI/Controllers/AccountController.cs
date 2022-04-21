using Application.Functions.Commands;
using Application.Functions.Commands.UserCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;

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

        [HttpPost]
        [SwaggerOperation(Summary = "Register user, no steam profile info needed")]
        public async Task<IActionResult> Post([FromBody] RegisterUserCommand command)
        {


            return Ok();
        }
    }
}
