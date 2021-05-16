using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Finance.Application.DtoModels.Base;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.DtoModels.User;
using Finance.Domain.Models;

namespace Finance.Application.DtoModels.Transaction
{
    public class TransactionGetDto : BaseDto
    {
        [Required]
        public double Money { get; set; }

        [Required]
        public bool TransactionStatus { get; set; }
        
        public string  TransactionType { get; set; }
        
        public bool IsCanceled { get; set; }

        public DateTime? DateTransaction { get; set; }
        
        public UserProfileDto? UserProfileDto { get; set; }
    }
}