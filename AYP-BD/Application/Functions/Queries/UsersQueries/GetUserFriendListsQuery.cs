using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Domain.Data.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Linq.Expressions;

namespace Application.Functions.Queries.UsersQueries
{
    public class GetUserFriendListsQuery : IRequest<PageResult<FriendDetailsDto>>
    {
        public string SteamID { get; set; }
        public FriendsListQueryParams QueryParams { get; set; }
        public GetUserFriendListsQuery(string steamID, FriendsListQueryParams queryParams)
        {
            SteamID = steamID;
            QueryParams = queryParams;
        }
    }
    public class GetUserFriendListsQueryHanlder : IRequestHandler<GetUserFriendListsQuery, PageResult<FriendDetailsDto>>
    {
        private readonly IHttpRequestHandler _httpHandler;
        private readonly string STEAM_FRIENDSLIST_PATH = "ISteamUser/GetFriendList/v1/";
        private readonly string STEAM_PLAYERSSUMMARIES_PATH = "ISteamUser/GetPlayerSummaries/v2/";
        public GetUserFriendListsQueryHanlder(IHttpRequestHandler httpHandler, IUsersRepostiory usersRepostiory)
        {
            _httpHandler = httpHandler;
        }

        public async Task<PageResult<FriendDetailsDto>> Handle(GetUserFriendListsQuery request, CancellationToken cancellationToken)
        {
            List<FriendDetailsDto> userFriendList = new();

            var friensListRequest = await _httpHandler.Get<FriendListDto>(STEAM_FRIENDSLIST_PATH, new
            {
                steamid = request.SteamID,
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

                    userFriendList.AddRange(getFriendsSummaries.Model.Response.Players
                        .Where(x => x.SteamId != request.SteamID)?
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
                        TimeCreated = x.AccountCreated.UnixTimeStampToDateTime()
                    }));
                }
            }
            else
            {
                return new PageResult<FriendDetailsDto>(null, 0, 0, 0);
            }
            userFriendList = userFriendList
                .Where(x => request.QueryParams.SearchPhrase == null ||
                (x.NickName.ToLower().Contains(request.QueryParams.SearchPhrase.ToLower())))
                .ToList();

            var userFriendListCount = userFriendList.Count;

            if (!string.IsNullOrEmpty(request.QueryParams.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<FriendDetailsDto, object>>>
                {
                    {nameof(FriendDetailsDto.IsOnline).ToLower(), r=>r.IsOnline },
                    {nameof(FriendDetailsDto.NickName).ToLower(), r=>r.NickName },
                    {nameof(FriendDetailsDto.TimeCreated).ToLower(), r=>r.TimeCreated }
                };
                if(request.QueryParams.SortBy.ToLower() == nameof(FriendDetailsDto.IsOnline).ToLower())
                {
                    var selectedColumn = columnsSelectors[request.QueryParams.SortBy.ToLower()];

                    userFriendList = request.QueryParams.SortDirection == SortDirection.ASC ?
                        userFriendList.AsQueryable().OrderBy(selectedColumn).ThenBy(x => x.NickName).ToList()
                        : userFriendList.AsQueryable().OrderByDescending(selectedColumn).ToList();
                }
                else
                {
                    var selectedColumn = columnsSelectors[request.QueryParams.SortBy.ToLower()];

                    userFriendList = request.QueryParams.SortDirection == SortDirection.ASC ?
                        userFriendList.AsQueryable().OrderBy(selectedColumn).ToList()
                        : userFriendList.AsQueryable().OrderByDescending(selectedColumn).ToList();
                }

            }
            else
            {
                userFriendList = userFriendList.OrderBy(x => x.IsOnline).ToList();
            }

            userFriendList = userFriendList
               .Skip(request.QueryParams.PageSize * (request.QueryParams.PageNumber - 1))
               .Take(request.QueryParams.PageSize).ToList();

            return new PageResult<FriendDetailsDto>(userFriendList, userFriendListCount, request.QueryParams.PageSize, request.QueryParams.PageNumber);
        }
    }
}

