using Application.DTO;

namespace Application.Services
{
    public interface IUsersFriendListService
    {
        Task<List<FriendDetailsDto>> GetFriendListFromAPI(string steamId, string phrase);
    }
}