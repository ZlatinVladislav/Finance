using Finance.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
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
