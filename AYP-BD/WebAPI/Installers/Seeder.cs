using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Installers
{
    public class Seeder
    {
        private readonly AypDbContext _context;
        private IPasswordHasher<User> _passwordHasher { get; }
        public Seeder(AypDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if(_context is not null && _context.Database.CanConnect())
            {
                if (_context.Database.IsRelational())
                {
                    //kc for integration tests
                    var pednimgMigrations = _context.Database.GetPendingMigrations();
                    if(pednimgMigrations != null)
                    {
                        _context.Database.Migrate();
                    }
                }
                if (!_context.Roles.Any())
                {
                    _context.Roles.AddRange(CreateRoles());
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> CreateRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id =  (byte)AccountTypes.User,
                    Name = "User"
                },
                new Role()
                {
                    Id =  (byte)AccountTypes.Admin,
                    Name = "Admin"
                }
            };
            return roles;
        }
    }
}
