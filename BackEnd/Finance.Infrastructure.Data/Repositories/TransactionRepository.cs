using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly DbSet<Transaction> _dbSet;

        public TransactionRepository(FinanceDBContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Transaction>();
        }

        public async Task<IQueryable<Transaction>> GetTransactionsForeignData(string id, DateTime date)
        {
            return _dbSet
                .Where(d => d.DateTransaction <= date)
                .Where(i => i.AppUser.Id == id)
                .Include(t => t.TransactionType)
                .Include(u => u.AppUser)
                .Include(b => b.Banks)
                .ThenInclude(bm => bm.Bank)
                .OrderByDescending(d => d.DateTransaction)
                .AsQueryable();
        }

        public async Task<IReadOnlyList<Transaction>> GetTransactionsForeignData()
        {
            return await _dbSet
                .Include(t => t.TransactionType)
                .Include(u => u.AppUser)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionsForeignDataById(Guid id)
        {
            return await _dbSet
                .Include(t => t.TransactionType)
                .Include(u => u.AppUser)
                .Include(b => b.Banks)
                .ThenInclude(bm => bm.Bank)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Transaction> GetTransactionsForeignDataByIdNoTracking(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(t => t.TransactionType)
                .Include(u => u.AppUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}