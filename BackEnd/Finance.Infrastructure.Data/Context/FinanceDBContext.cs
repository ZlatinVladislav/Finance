using Finance.Domain.ConfigTable;
using Finance.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Context
{
    public class FinanceDBContext : IdentityDbContext<AppUser>
    {
        public FinanceDBContext(DbContextOptions<FinanceDBContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankTransaction> TransactionBanks { get; set; }
        public DbSet<UserDescription> UserDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TransactionConfig());
            modelBuilder.ApplyConfiguration(new BankTransactionConfig());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfig());
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new UserDescriptionConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) base.OnConfiguring(optionsBuilder);
        }
    }
}