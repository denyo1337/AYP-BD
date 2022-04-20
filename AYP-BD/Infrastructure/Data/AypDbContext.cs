using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AypDbContext : DbContext
    {
        public AypDbContext(DbContextOptions options) : base(options)
        {

        }
        //db sets here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
