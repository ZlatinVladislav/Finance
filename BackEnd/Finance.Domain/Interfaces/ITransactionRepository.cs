using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models;
using Finance.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Domain.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IReadOnlyList<Transaction>> GetTransactionsForeignData(string id);
        Task<IReadOnlyList<Transaction>> GetTransactionsForeignData();
        Task<Transaction> GetTransactionsForeignDataById(Guid id);
        Task<Transaction> GetTransactionsForeignDataByIdNoTracking(Guid id);
    }
}
