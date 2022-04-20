using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired()
                .HasForeignKey(p => p.RoleId);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(x => x.NickName)
                .HasMaxLength(50);
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(15);
        }
    }
}
