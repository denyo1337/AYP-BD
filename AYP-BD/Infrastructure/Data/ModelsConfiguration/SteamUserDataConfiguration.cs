using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ModelsConfiguration
{
    public class SteamUserDataConfiguration : IEntityTypeConfiguration<SteamUserData>
    {
        public void Configure(EntityTypeBuilder<SteamUserData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            builder.HasOne(x => x.User)
                .WithOne(x => x.SteamUserData)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey<SteamUserData>(x => x.UserId);
            builder.Property(x => x.PersonName)
                .HasColumnName(nameof(SteamUserData.PersonName));
        }
    }
}
