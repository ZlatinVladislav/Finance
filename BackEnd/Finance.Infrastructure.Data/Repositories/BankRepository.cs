using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        private readonly DbSet<Transaction> _dbSet;
      
        public BankRepository(FinanceDBContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Transaction>();
        }
    }
}