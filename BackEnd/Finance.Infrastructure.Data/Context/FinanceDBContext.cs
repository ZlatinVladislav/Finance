using Finance.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Finance.Domain.ConfigTable;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TransactionConfig());
            modelBuilder.ApplyConfiguration(new BankTransactionConfig());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfig());
            // modelBuilder.Configurations.AddFromAssembly(typeof(FinanceDBContext).Assembly);
            // modelBuilder.Entity<BankTransaction>()
            //     .HasKey(lc => new { lc.BankId, lc.TransactionId });
            // modelBuilder.Entity<BankTransaction>()
            //     .Ignore(x => x.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}