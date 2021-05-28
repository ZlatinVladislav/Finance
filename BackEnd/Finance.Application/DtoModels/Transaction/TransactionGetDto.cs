using System;
using System.Collections.Generic;
using Finance.Application.DtoModels.Bank;
using Finance.Application.DtoModels.Base;
using Finance.Application.DtoModels.User;

namespace Finance.Application.DtoModels.Transaction
{
    public class TransactionGetDto : BaseDto
    {
        public double Money { get; set; }
        public bool TransactionStatus { get; set; }
        public string TransactionType { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? DateTransaction { get; set; }
        public UserProfileDto? UserProfileDto { get; set; }
        public ICollection<BankDto> BankDto { get; set; }
    }
}