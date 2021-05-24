using System.Threading.Tasks;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces.Base;

namespace Finance.Infrastructure.Data.Interfaces
{
    public interface ITransactionTypeRepository : IBaseRepository<TransactionType>
    {
        Task<TransactionType> GetTransactionTypeByName(string type);
    }
}