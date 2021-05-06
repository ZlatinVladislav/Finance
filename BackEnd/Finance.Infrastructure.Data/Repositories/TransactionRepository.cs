using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly FinanceDBContext _context;
        private readonly DbSet<Transaction> _dbSet;
      
        public TransactionRepository(FinanceDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<Transaction>();
        }

        public async Task<IReadOnlyList<Transaction>> GetTransactionsForeignData()
        {
            return await _context.Set<Transaction>()
                .Include(t=>t.TransactionType)
                .Include(u=>u.AppUser)
                .ToListAsync();
        }
        
        public async Task<Transaction> GetTransactionsForeignDataById(Guid id)
        {
            return await _context.Set<Transaction>()
                .Include(t=>t.TransactionType)
                .Include(u=>u.AppUser)
                .SingleOrDefaultAsync(x=>x.Id==id);
                
        }
    }
}
