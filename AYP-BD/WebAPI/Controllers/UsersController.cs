using Application.Functions.Commands.UserCommands;
using Application.Functions.Queries.UsersQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("ayb/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register user, no steam profile info needed")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("sign-in")]
        [SwaggerOperation(Summary = "Sing in into application using email & password")]
        public async Task<IActionResult> LogIn([FromBody] SignInUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet("validate/email/{email}")]
        [SwaggerOperation(Summary = "Check if email is already taken")]
        public async Task<IActionResult> ValidateEmailIsTaken([FromRoute] string email)
        {
            return Ok(await _mediator.Send(new IsEmailTakenQuery(email)));
        }
    }
}
