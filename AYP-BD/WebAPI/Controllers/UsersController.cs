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
    [AllowAnonymous]
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
        [HttpGet("validate/nickName/{nickName}")]
        [SwaggerOperation(Summary = "Endpoint to check if nickname is alrady taken")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> ValidateNickName([FromRoute] string nickName)
        {
            return Ok(await _mediator.Send(new IsNickNameTakenQuery(nickName)));
        }
        [HttpGet("searchPlayer")]
        [SwaggerOperation(Summary = "Get user by  name specified in url or profile name")]
        [Produces(typeof(PlayerDto))]
        public async Task<IActionResult> GetUserByNameOrSteamId([FromQuery] string phrase)
        {
            var profile = await _mediator.Send(new GetUserSteamProfileBySteamIdOrNick(phrase));
            return profile is not null ? Ok(profile) : NotFound();
        }


        [HttpGet("friendsLists/{steamId}")]
        [SwaggerOperation(Summary = "Endpoint to return user friends list")]
        [Produces(typeof(PageResult<FriendDetailsDto>))]
        public async Task<IActionResult> GetFriendsList([FromRoute]string steamId, [FromQuery] FriendsListQueryParams queryParams)
        {
            var result = await _mediator.Send(new GetUserFriendListsQuery(steamId, queryParams));
            return Ok(result);
        }




        [HttpGet("validate/steamId/{steamId:long}")]
        [SwaggerOperation(Summary = "validate your steamid ")]
        [Produces(typeof(SteamIdValidationResult))]
        public async Task<IActionResult> ValidateSteamId([FromRoute]long steamId)
        {
            return Ok(await _mediator.Send(new IsSteamIdValidQuery(steamId)));
        }


    }
}
