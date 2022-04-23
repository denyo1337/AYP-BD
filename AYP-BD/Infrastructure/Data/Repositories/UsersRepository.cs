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

        public async Task<User> GetUser(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
