using System;
using Finance.Application.DtoModels.Base;

namespace Finance.Application.DtoModels.Bank
{
    public class BankTransactionDto: BaseDto 
    {
        public Guid TransactionId { get; set; }
    }
}