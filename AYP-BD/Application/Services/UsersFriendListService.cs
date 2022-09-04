using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class UsersFriendListService : IUsersFriendListService
    {
        private readonly IHttpRequestHandler _httpHandler;
        private readonly string STEAM_FRIENDSLIST_PATH = "ISteamUser/GetFriendList/v1/";
        private readonly string STEAM_PLAYERSSUMMARIES_PATH = "ISteamUser/GetPlayerSummaries/v2/";

        public UsersFriendListService(IHttpRequestHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<List<FriendDetailsDto>> GetFriendListFromAPI(string steamId, string phrase)
        {
            var friendList = new List<FriendDetailsDto>();

            var friensListRequest = await _httpHandler.Get<FriendListDto>(STEAM_FRIENDSLIST_PATH, new
            {
                steamid = steamId,
                relationship = "friend"
            });

            if (friensListRequest.StatusCode == StatusCodes.Status200OK && friensListRequest.Model.FriendsList.Friends.Any())
            {
                var friendsSteamIds = friensListRequest.Model.FriendsList.Friends.Select(x => x.SteamId).ToList();

                for (int i = 0; i < friendsSteamIds.Count; i += 100)
                {

                    var getFriendsSummaries = await _httpHandler.Get<UserSteamDtaDto>(STEAM_PLAYERSSUMMARIES_PATH, new
                    {
                        steamids = string.Join(",", friendsSteamIds.Skip(i).Take(100))
                    });

                    friendList.AddRange(getFriendsSummaries.Model.Response.Players
                        .Where(x => x.SteamId != steamId)?
                        .Select(x =>
                    new FriendDetailsDto
                    {
                        AvatarFull = x.AvatarfullUrl,
                        SteamId = x.SteamId,
                        IsOnline = x.IsOnline != 0,
                        Loccountrycode = x.SteamNationality,
                        NickName = x.SteamNickName,
                        ProfileUrl = x.ProfileUrl,
                        RealName = x.RealName,
                        TimeCreated = x.AccountCreated.UnixTimeStampToDateTime(),
                        Communityvisibilitystate = x.Communityvisibilitystate,
                    }));
                }
            }
            else
            {
                return null;
            }

            return FilterBySearchPhrase(friendList, phrase);
        }

        private List<FriendDetailsDto> FilterBySearchPhrase(List<FriendDetailsDto> source, string phrase)
        {
            var phareLowered = phrase?.ToLower();
            return source
                    .Where(x => phareLowered == null || x.NickName.ToLower().Contains(phareLowered))
                    .ToList();
        }
    }
}
