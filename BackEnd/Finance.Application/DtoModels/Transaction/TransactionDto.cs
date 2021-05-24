using System;
using System.ComponentModel.DataAnnotations;
using Finance.Application.DtoModels.Base;
using Finance.Application.DtoModels.User;

namespace Finance.Application.DtoModels.Transaction
{
    public class TransactionDto : BaseDto
    {
        [Required]
        public double Money { get; set; }

        [Required]
        public bool TransactionStatus { get; set; }

        [Required]
        public Guid TransactionTypeId { get; set; }

        [Required]
        public bool IsCanceled { get; set; }

        [Required]
        public DateTime? DateTransaction { get; set; }

        public UserProfileDto? UserProfileDto { get; set; }
        public Domain.Models.Bank? BankDto { get; set; }
    }
}