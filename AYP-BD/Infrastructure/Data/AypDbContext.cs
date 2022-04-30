using Domain.Models;
using Infrastructure.Data.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AypDbContext : DbContext
    {
        public AypDbContext(DbContextOptions options) : base(options)
        {

        }
        //db sets here
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SteamUserData> SteamUserDatas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new SteamUserDataConfiguration());
        }
    }
}
