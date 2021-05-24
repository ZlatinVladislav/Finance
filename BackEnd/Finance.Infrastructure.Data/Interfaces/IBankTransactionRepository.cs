using System;
using System.Threading.Tasks;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces.Base;

namespace Finance.Infrastructure.Data.Interfaces
{
    public interface IBankTransactionRepository: IBaseRepository<BankTransaction>
    {
        Task<bool> CheckIfExist(Guid bankId,Guid transactionId);
    }
}