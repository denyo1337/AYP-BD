using Application.DTO;
using Application.Functions.Queries.UsersQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("ayb/api/[controller]")]
    public class StatsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public StatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("userStats/{steamId}")]
        [SwaggerOperation(Summary = "Endpoint for fetching users stats by steamId")]
        [Produces(typeof(StatsDto))]
        public async Task<IActionResult> GetUserStats(string steamId)
        {
            var stats = await _mediator.Send(new GetUserStatsQuery(steamId));
            return stats is not null ? Ok(stats) : BadRequest();
        }
    }
}
