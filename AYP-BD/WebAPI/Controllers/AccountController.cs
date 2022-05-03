using Application.Common;
using Application.DTO;
using Application.Functions.Commands.UserCommands;
using Application.Functions.Queries.UsersQueries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("ayb/api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get the user account dto")]
        [Produces(typeof(AccountDetailsDto))]
        public async Task<IActionResult> GetAccountDetails()
        {
            return Ok(await _mediator.Send(new GetUserDetailsQuery()));
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update user account details")]
        [Produces(typeof(AccountDetailsDto))]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateAccountDetailsCommand dto)
        {
            var result = await _mediator.Send(dto);
            return result is not null ? Ok(result) : BadRequest();
        }

        [HttpPut("setSteamId")]
        [SwaggerOperation(Summary = "Update steamId for user account")]
        [Produces(typeof(SteamIdValidationResult))]
        public async Task<IActionResult> AssignSteamIdToUser([FromBody] AssignSteamIdToUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("updateSteamData")]
        [SwaggerOperation(Summary = "Update steamUserDetails")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdateSteamUserDetails()
        {
            var result = await _mediator.Send(new UpdateUserSteamDetailsCommand());

            return Ok(result);
        }
      
    }
}
