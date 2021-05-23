using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Domain.ConfigTable
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(x => x.Money).HasColumnType("float");
            builder.HasOne(x => x.TransactionType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.TransactionTypeId)
                .OnDelete(DeleteBehavior.SetNull); 
            
            builder.HasOne(x => x.AppUser)
              .WithMany(x => x.Transactions)
              .HasForeignKey(x => x.AppUserId);
        }
    }
}
