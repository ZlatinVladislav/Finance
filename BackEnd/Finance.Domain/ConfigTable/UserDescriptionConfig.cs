using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Domain.ConfigTable
{
    public class UserDescriptionConfig : IEntityTypeConfiguration<UserDescription>
    {
        public void Configure(EntityTypeBuilder<UserDescription> builder)
        {
            builder.HasKey(x => x.UserDescriptionId);
            builder.Ignore(x => x.Id);
        }
    }
}