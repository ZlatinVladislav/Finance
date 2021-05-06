using Finance.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Models
{
    public class TransactionType : BaseEntity
    {
        [Column(TypeName = "VARCHAR(20))")]
        public string TransactionTypes { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
