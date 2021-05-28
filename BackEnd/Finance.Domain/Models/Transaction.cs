using System;
using System.Collections.Generic;
using Finance.Domain.Models.Base;

namespace Finance.Domain.Models
{
    public class Transaction : BaseEntity
    {
        public double? Money { get; set; }
        public bool TransactionStatus { get; set; }
        public Guid? TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? DateTransaction { get; set; } = DateTime.Now;
        public virtual ICollection<BankTransaction> Banks { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}