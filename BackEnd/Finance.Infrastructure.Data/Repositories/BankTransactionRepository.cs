using System;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Context;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Data.Repositories
{
    public class BankTransactionRepository : BaseRepository<BankTransaction>, IBankTransactionRepository
    {
        private readonly DbSet<BankTransaction> _dbSet;

        public BankTransactionRepository(FinanceDBContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<BankTransaction>();
        }

        public async Task<bool> CheckIfExist(Guid bankId, Guid transactionId)
        {
            return _dbSet
                .Any(b => b.BankId == bankId && b.TransactionId == transactionId);
        }
    }
}