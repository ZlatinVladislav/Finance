using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Domain.ConfigTable
{
    public class TransactionParticipation: IEntityTypeConfiguration<TransactionParticipation>
    {
        public void Configure(EntityTypeBuilder<TransactionParticipation> builder)
        {
            //modelBuilder.Entity<TransactionParticipation>(x => x.HasKey(aa => new {aa.Id, aa.TransactionId}));
            // builder.HasOne(x=>x.Id)
            //     .WithMany(x => x.Transactions)
            //     .HasForeignKey(x => x.TransactionTypeId);          
        }
    }
}