using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Interfaces
{
    public interface IUsersRepostiory : IRepository<User>
    {
        Task<bool> AddUser(User user, CancellationToken cancellationToken);
        Task<User> GetUser(string email, CancellationToken cancellationToken);
        Task<bool> IsEmailTaken(string email, CancellationToken cancellationToken);
        Task<User> GetAccountDetails(long id, CancellationToken cancellationToken);
        Task<User> GetAccountDetailsWithSteamUserData(long id, CancellationToken cancellationToken);
        Task<bool> DeleteSteamUserData(SteamUserData id, CancellationToken cancellationToken);
        Task<bool> IsSteamIDTaken(long steamid, CancellationToken cancellationToken);
    }
}
