using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using System.Globalization;

namespace Application.Functions.Queries.UsersQueries
{
    public class GetUserSteamProfileBySteamIdOrNick : IRequest<PlayerDto>
    {
        public string Phrase { get; set; }

        public GetUserSteamProfileBySteamIdOrNick(string phrase)
        {
            Phrase = phrase;
        }
    }
    public class GetUserSteamProfileBySteamIdOrNickHandler : IRequestHandler<GetUserSteamProfileBySteamIdOrNick, PlayerDto>
    {
        private readonly IHttpRequestHandler _httpHandler;
        private readonly string VANITY_USER_PATH = "ISteamUser/ResolveVanityURL/v1/";
        private readonly string USERDATA_PATH = "ISteamUser/GetPlayerSummaries/v2/";
        private const int VanityUrlTypesLimit = 4;
        public GetUserSteamProfileBySteamIdOrNickHandler(IHttpRequestHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<PlayerDto> Handle(GetUserSteamProfileBySteamIdOrNick request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Phrase)) return null;

            if (long.TryParse(request.Phrase, out long steamId))
            {
                var user = await _httpHandler.Get<UserSteamDtaDto>(USERDATA_PATH, new
                {
                    steamids = steamId
                });

                return user.Model.Response.Players.Select(x => new PlayerDto
                {
                    AccountCreated = x.AccountCreated.UnixTimeStampToDateTime().ToString("g", CultureInfo.GetCultureInfo("de-DE")),
                    AvatarfullUrl = x.AvatarfullUrl,
                    IsOnline = x.IsOnline == 1,
                    ProfileUrl = x.ProfileUrl,
                    RealName = x.RealName,
                    SteamId = x.SteamId,
                    SteamNationality = x.SteamNationality,
                    SteamNickName = x.SteamNickName,
                }).FirstOrDefault();
            }
            for (int i = 1; i < VanityUrlTypesLimit; i++)
            {
                var result = await _httpHandler.Get<GetUserSteamIdDto>(VANITY_USER_PATH, new
                {
                    vanityurl = request.Phrase,
                    url_type = i
                });
                if (result.StatusCode == 200 && result.Model.Response.Success == (int)GetUserResponseTypes.Success)
                {
                    var user = await _httpHandler.Get<UserSteamDtaDto>(USERDATA_PATH, new
                    {
                        steamids = result.Model.Response.SteamId
                    });
                    if (user.Model.Response.Players.Any())
                    {
                        return user.Model.Response.Players.Select(x => new PlayerDto
                        {
                            AccountCreated = x.AccountCreated.UnixTimeStampToDateTime().ToString("g", CultureInfo.GetCultureInfo("de-DE")),
                            AvatarfullUrl = x.AvatarfullUrl,
                            IsOnline = x.IsOnline == 1,
                            ProfileUrl = x.ProfileUrl,
                            RealName = x.RealName,
                            SteamId = x.SteamId,
                            SteamNationality = x.SteamNationality,
                            SteamNickName = x.SteamNickName,
                        }).FirstOrDefault();
                    }
                }
            }

            return null;
        }
    }
}
