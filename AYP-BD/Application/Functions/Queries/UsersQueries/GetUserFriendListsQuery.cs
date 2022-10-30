using Application.Common;
using Application.DTO;
using Application.Services;
using MediatR;
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
    public class GetUserFriendListsQueryHandler : IRequestHandler<GetUserFriendListsQuery, PageResult<FriendDetailsDto>>
    {
        private readonly IUsersFriendListService _userService;

        public GetUserFriendListsQueryHandler(IUsersFriendListService userService)
        {
            _userService = userService;
        }

        public async Task<PageResult<FriendDetailsDto>> Handle(GetUserFriendListsQuery request, CancellationToken cancellationToken)
        {
            List<FriendDetailsDto> userFriendList = await _userService.GetFriendListFromAPI(request.SteamID, request.QueryParams.SearchPhrase);
            if (userFriendList == null || userFriendList?.Count == 0)
            {
                return new PageResult<FriendDetailsDto>(null, 0, request.QueryParams.PageSize, request.QueryParams.PageNumber);
            }

            SortCollection(ref userFriendList,
                request.QueryParams.SortBy ?? nameof(FriendDetailsDto.NickName), request.QueryParams.SortDirection);

            PaginateResult(ref userFriendList, request.QueryParams.PageSize, request.QueryParams.PageNumber);

            return new PageResult<FriendDetailsDto>(userFriendList, userFriendList.Count, request.QueryParams.PageSize, request.QueryParams.PageNumber);
        }
        private static void SortCollection(ref List<FriendDetailsDto> source, string sortBy, SortDirection sortDirection)
        {
            var sortbyLowered = sortBy.ToLower();
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<FriendDetailsDto, object>>>
                {
                    {nameof(FriendDetailsDto.IsOnline).ToLower(), r=>r.IsOnline },
                    {nameof(FriendDetailsDto.NickName).ToLower(), r=>r.NickName },
                    {nameof(FriendDetailsDto.TimeCreated).ToLower(), r=>r.TimeCreated }
                };
                if (sortbyLowered == nameof(FriendDetailsDto.IsOnline).ToLower())
                {
                    var selectedColumn = columnsSelectors[sortbyLowered];

                    source = sortDirection == SortDirection.ASC ?
                        source.AsQueryable().OrderBy(selectedColumn).ThenBy(x => x.NickName).ToList()
                        : source.AsQueryable().OrderByDescending(selectedColumn).ToList();
                }
                else
                {
                    var selectedColumn = columnsSelectors[sortbyLowered];

                    source = sortDirection == SortDirection.ASC ?
                            source.AsQueryable().OrderBy(selectedColumn).ToList()
                            : source.AsQueryable().OrderByDescending(selectedColumn).ToList();
                }
            }
            else
            {
                source = source.OrderBy(x => x.IsOnline).ToList();
            }
        }

        private static void PaginateResult(ref List<FriendDetailsDto> source, int pageSize, int pageNumber)
        {
            source.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}

