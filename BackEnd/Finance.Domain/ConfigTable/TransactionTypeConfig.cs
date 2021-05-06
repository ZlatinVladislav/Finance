using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Domain.ConfigTable
{
    public class TransactionTypeConfig: IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            //builder.Property(x => x.Id).("VARCHAR(20))");    
            builder.Property(x => x.TransactionTypes).HasColumnType("VARCHAR(20))");           
        }
    }
}
