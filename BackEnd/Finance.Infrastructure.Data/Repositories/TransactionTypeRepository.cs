using System;
using System.Threading.Tasks;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class TransactionTypeRepository: BaseRepository<TransactionType>, ITransactionTypeRepository
    {
        private readonly DbSet<TransactionType> _dbSet;
      
        public TransactionTypeRepository(FinanceDBContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TransactionType>();
        }
        
        public async Task<TransactionType> GetTransactionTypeByName(string type)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.TransactionTypes.Equals(type));
        }
    }
}