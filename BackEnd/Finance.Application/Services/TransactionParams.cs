using System;

namespace Finance.Application.Services
{
    public class TransactionParams:PagingParams
    {
        public bool? TransactionStatus { get; set; }
        public DateTime StartDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

    }
}