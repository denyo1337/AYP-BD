﻿using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
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
                var user = await _httpHandler.Get<UserSteamDataDto>(USERDATA_PATH, new
                {
                    steamids = steamId
                });
                var mappedUser = user.Model.Response.Players?.Select(x => new PlayerDto
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
                if (mappedUser != null)
                    return mappedUser;
            }
            for (int i = 1; i < VanityUrlTypesLimit; i++)
            {
                Response<GetUserSteamIdDto> result = await GetUserIdWithSpecificVanityId(request, i);
                if (result.StatusCode == StatusCodes.Status200OK && result.Model.Response.Success == (int)GetUserResponseTypes.Success)
                {
                    var user = await _httpHandler.Get<UserSteamDataDto>(USERDATA_PATH, new
                    {
                        steamids = result.Model.Response.SteamId
                    });
                    return user.Model.Response?.Players?.Select(x => new PlayerDto
                    {
                        AccountCreated = x.AccountCreated.UnixTimeStampToDateTime()
                                .ToString("g", CultureInfo.GetCultureInfo("de-DE")),
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
            return new();
        }
        private async Task<Response<GetUserSteamIdDto>> GetUserIdWithSpecificVanityId(GetUserSteamProfileBySteamIdOrNick request, int vanityId)
        {
            return await _httpHandler.Get<GetUserSteamIdDto>(VANITY_USER_PATH, new
            {
                vanityurl = request.Phrase,
                url_type = vanityId
            });
        }
    }
}
