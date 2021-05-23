using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Domain.ConfigTable
{
    public class BankTransactionConfig : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.HasKey(x => new {x.BankId, x.TransactionId});
            builder.Ignore(x => x.Id);
            builder.ToTable("BankTransaction");

            builder.HasOne(x => x.Transaction)
                .WithMany(x => x.Banks)
                .HasForeignKey(x => x.TransactionId);
                // .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Bank)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.BankId);
            // .OnDelete(DeleteBehavior.SetNull);
        }
    }
}