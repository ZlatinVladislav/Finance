using Finance.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Models
{
    public class Transaction : BaseEntity
    {
        [Column(TypeName = "float")]
        public double? Money { get; set; }

        public bool TransactionStatus { get; set; }
        public Guid TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public bool IsCanceled { get; set; }
        
        public DateTime? DateTransaction { get; set; } = DateTime.Now;
        public virtual AppUser AppUser { get; set; }
      
    }
}