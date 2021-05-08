using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Domain.Interfaces
{
    public interface ITransactionTypeRepository : IBaseRepository<TransactionType>
    {
        Task<TransactionType> GetTransactionTypeByName(string type);
    }
}
