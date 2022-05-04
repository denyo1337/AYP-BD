using Application.Common;
using Application.DTO;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions.Queries.UsersQueries
{
    public class GetUserStatsQuery : IRequest<StatsDto>
    {
        public string SteamId { get; set; }

        public GetUserStatsQuery(string steamId)
        {
            SteamId = steamId;
        }
    }
    public class GetUserStatsQueryHandler : IRequestHandler<GetUserStatsQuery, StatsDto>
    {
        private readonly IHttpRequestHandler _httpHandler;
        private readonly IConfiguration _configuration;
        private readonly string STEAM_USERSTATS_PATH = "ISteamUserStats/GetUserStatsForGame/v2/";
        private const int APP_ID = 730;

        public GetUserStatsQueryHandler(IHttpRequestHandler httpHandler, IConfiguration configuration)
        {
            _httpHandler = httpHandler;
            _configuration = configuration;
        }

        public async Task<StatsDto> Handle(GetUserStatsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.SteamId) || !long.TryParse(request.SteamId, out _)) return null;

            var resp = await _httpHandler.Get<PlayerCSGOStats>(STEAM_USERSTATS_PATH, new
            {
                appid = APP_ID,
                steamid = request.SteamId
            });

            var names = _configuration["StatsNames"].ToString().Split(",");
            if(resp.StatusCode != StatusCodes.Status200OK || resp.Model is null) return null;

            var data = resp.Model.PlayerStats.Stats.Where(x => names.Contains(x.Name)).ToList();

            return new(data);

        }
    }
}


