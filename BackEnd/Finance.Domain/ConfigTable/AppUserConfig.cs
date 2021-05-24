using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Domain.ConfigTable
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .HasOne(a => a.UserDescription)
                .WithOne(s => s.User)
                .HasForeignKey<UserDescription>(a => a.UserDescriptionId);
        }
    }
}