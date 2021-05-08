﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly DbSet<Transaction> _dbSet;
      
        public TransactionRepository(FinanceDBContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Transaction>();
        }

        public async Task<IReadOnlyList<Transaction>> GetTransactionsForeignData(string id)
        {
            return await _dbSet
                .Include(t=>t.TransactionType)
                .Include(u=>u.AppUser)
                .Where(i=>i.AppUser.Id==id)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Transaction>> GetTransactionsForeignData()
        {
            return await _dbSet
                .Include(t=>t.TransactionType)
                .Include(u=>u.AppUser)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionsForeignDataById(Guid id)
        {
            return await _dbSet
                .Include(t=>t.TransactionType)
                .Include(u=>u.AppUser)
                .SingleOrDefaultAsync(x=>x.Id==id);
                
        }

        public async Task<Transaction> GetTransactionsForeignDataByIdNoTracking(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(t=>t.TransactionType)
                .Include(u=>u.AppUser)
                .SingleOrDefaultAsync(x=>x.Id==id);
        }
    }
}