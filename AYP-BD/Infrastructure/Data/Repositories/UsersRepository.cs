using Domain.Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UsersRepository : RepositoryBase<User, AypDbContext>, IUsersRepostiory
    {
        public UsersRepository(AypDbContext context) : base(context)
        {
        }

        protected override DbSet<User> EntitySet => _context.Users;

        public async Task<bool> AddUser(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteSteamUserData(SteamUserData steamUserData, CancellationToken cancellationToken)
        {
            _context.SteamUserDatas.Remove(steamUserData);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public Task<User> GetAccountDetails(long id, CancellationToken cancellationToken)
        {
            return _context.Users
                .Include(x => x.SteamUserData)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<User> GetAccountDetailsWithSteamUserData(long id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(x => x.SteamUserData)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken:cancellationToken);
        }

        public async Task<User> GetUser(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> IsEmailTaken(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Email == email, cancellationToken: cancellationToken);
        }
        public Task<bool> IsSteamIDTaken(long steamid, CancellationToken cancellationToken)
        {
            return  _context.Users.AnyAsync(x => x.SteamId == steamid, cancellationToken: cancellationToken);
        }
    }
}
