using Application.DTO;
using Application.Functions.Commands.UserCommands;
using Application.Functions.Queries.UsersQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateAccountDetailsCommand dto)
        {
            var result = await _mediator.Send(dto);
            return StatusCode(204, new { id = "1" });
        }

        [HttpPut("setSteamId")]
        [SwaggerOperation(Summary = "Update steamId for user account")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> AssignSteamIdToUser([FromBody] AssignSteamIdToUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok(result) : BadRequest();
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
