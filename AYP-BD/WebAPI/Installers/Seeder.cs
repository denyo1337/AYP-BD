using Application.Common;
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
        private IEntityGenerator _entityGenerator { get; }
        private IConfiguration _configuration;
        public Seeder(AypDbContext context, IPasswordHasher<User> passwordHasher, IEntityGenerator entityGenerator, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _entityGenerator = entityGenerator;
            _configuration = configuration;
        }
        public void Seed()
        {
            if (_context is not null && _context.Database.CanConnect())
            {
                if (_context.Database.IsRelational())
                {
                    //kc for integration tests
                    var pednimgMigrations = _context.Database.GetPendingMigrations();
                    if (pednimgMigrations != null)
                    {
                        _context.Database.Migrate();
                    }
                }
                if (!_context.Roles.Any())
                {
                    _context.Roles.AddRange(CreateRoles());
                    _context.SaveChanges();
                }
                if (!_context.Users.Any(x => x.RoleId == (byte)AccountTypes.Admin))
                {
                    _context.Users.Add(CreateAdmin());
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
        private User CreateAdmin()
        {
            var admin = new User()
            {
                Id = _entityGenerator.Generate(),
                Email = "admin@gmail.com",
                NickName = "admin",
                Nationality = "admin",
                Created = DateTime.Now,
                IsActive = true,
                IsBanned = false,
                LastModified = DateTime.Now,
                RoleId = (byte)AccountTypes.Admin,
            };
            admin.PasswordHash = _passwordHasher.HashPassword(admin, _configuration["DefaultPassword"]);
            return admin;
        }
     }
    }

