using System.Collections.Generic;
using Finance.Domain.Models.Base;

namespace Finance.Domain.Models
{
    public class Bank : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<BankTransaction> Transactions { get; set; }
    }
}