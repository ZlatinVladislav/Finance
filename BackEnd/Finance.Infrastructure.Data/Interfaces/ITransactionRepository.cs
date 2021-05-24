using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces.Base;

namespace Finance.Infrastructure.Data.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IQueryable<Transaction>> GetTransactionsForeignData(string id, DateTime date);
        Task<IReadOnlyList<Transaction>> GetTransactionsForeignData();
        Task<Transaction> GetTransactionsForeignDataById(Guid id);
        Task<Transaction> GetTransactionsForeignDataByIdNoTracking(Guid id);
    }
}