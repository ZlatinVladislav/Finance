using System.Collections.Generic;
using Finance.Domain.Models.Base;

namespace Finance.Domain.Models
{
    public class TransactionType : BaseEntity
    {
        public string TransactionTypes { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}