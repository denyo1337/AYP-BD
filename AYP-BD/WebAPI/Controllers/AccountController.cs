using Application.DTO;
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
        [SwaggerOperation(Summary = "Register user, no steam profile info needed")]
        [Produces(typeof(AccountDetailsDto))]
        public async Task<IActionResult> GetAccountDetails()
        {
            return Ok(await _mediator.Send(new GetUserDetailsQuery()));
        }
    }
}
