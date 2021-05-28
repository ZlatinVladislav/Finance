using System;
using Finance.Domain.Models.Base;

namespace Finance.Domain.Models
{
    public class BankTransaction : BaseEntity
    {
        public Guid? BankId { get; set; }
        public Bank Bank { get; set; }
        public Guid? TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}