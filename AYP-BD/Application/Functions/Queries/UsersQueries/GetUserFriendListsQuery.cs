using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Domain.Data.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions.Queries.UsersQueries
{
    public class GetUserFriendListsQuery : IRequest<IList<FriendDetailsDto>>
    {
        public string SteamID { get; set; }

        public GetUserFriendListsQuery(string steamID)
        {
            SteamID = steamID;
        }
    }
    public class GetUserFriendListsQueryHanlder : IRequestHandler<GetUserFriendListsQuery, IList<FriendDetailsDto>>
    {
        private readonly IHttpRequestHandler _httpHandler;
        private readonly IUsersRepostiory _usersRepostiory;
        private readonly string STEAM_FRIENDSLIST_PATH = "ISteamUser/GetFriendList/v1/";
        private readonly string STEAM_PLAYERSSUMMARIES_PATH = "ISteamUser/GetPlayerSummaries/v2/";
        public GetUserFriendListsQueryHanlder(IHttpRequestHandler httpHandler, IUsersRepostiory usersRepostiory)
        {
            _httpHandler = httpHandler;
            _usersRepostiory = usersRepostiory;
        }

        public async Task<IList<FriendDetailsDto>> Handle(GetUserFriendListsQuery request, CancellationToken cancellationToken)
        {
            var friensListRequest = await _httpHandler.Get<FriendListDto>(STEAM_FRIENDSLIST_PATH, new
            {
                steamid = request.SteamID,
                relationship = "friend"
            });

            if (friensListRequest.StatusCode == StatusCodes.Status200OK && friensListRequest.Model.FriendsList.Friends.Any())
            {
                var friendsSteamIds = friensListRequest.Model.FriendsList.Friends.Select(x => x.SteamId).ToList();
                var idsTopath = string.Join(',', friendsSteamIds);

                var getFriendsSummaries = await _httpHandler.Get<UserSteamDtaDto>(STEAM_PLAYERSSUMMARIES_PATH, new
                {
                    steamids = idsTopath
                });
                return getFriendsSummaries.Model.Response.Players.Where(x => x.SteamId != request.SteamID)?.Select(x => new FriendDetailsDto
                {
                    AvatarFull = x.AvatarfullUrl,
                    SteamId = x.SteamId,
                    IsOnline = x.IsOnline == 0 ? false : true,
                    Loccountrycode = x.SteamNationality,
                    NickName = x.SteamNickName,
                    ProfileUrl = x.ProfileUrl,
                    RealName = x.RealName,
                    TimeCreated =  x.AccountCreated.UnixTimeStampToDateTime().ToString("g", CultureInfo.GetCultureInfo("de-DE"))
                }).ToList();
            }
            else
            {
                return new List<FriendDetailsDto>();
            }
        }
    }
}

