using Application.DTO;
using Application.Interfaces;
using Domain.Data.Interfaces;
using MediatR;

namespace Application.Functions.Queries.UsersQueries
{
    public class IsSteamIdValidQuery : IRequest<bool>
    {
        public long SteamId { get; set; }

        public IsSteamIdValidQuery(long steamId)
        {
            SteamId = steamId;
        }
    }

    public class IsSteamIdValidQueryHandler : IRequestHandler<IsSteamIdValidQuery, bool>
    {
        private readonly IHttpRequestHandler _httpHandler;
        private readonly IUsersRepostiory _usersRepostiory;

        private const string USER_DETAILS_PATH = "ISteamUser/GetPlayerSummaries/v2/";

        public IsSteamIdValidQueryHandler(IHttpRequestHandler httpHandler, IUsersRepostiory usersRepostiory)
        {
            _httpHandler = httpHandler;
            _usersRepostiory = usersRepostiory;
        }

        public async Task<bool> Handle(IsSteamIdValidQuery request, CancellationToken cancellationToken)
        {
            var player = await _httpHandler.Get<UserSteamDtaDto>(USER_DETAILS_PATH, new { steamids = request.SteamId });

            if (player.Model.Response.Players.Any() && !await _usersRepostiory.IsSteamIDTaken(request.SteamId, cancellationToken))
            {
                return true;
            }

            return false;
        }
    }
}
