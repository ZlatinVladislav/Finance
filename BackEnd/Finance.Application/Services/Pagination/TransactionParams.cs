using System;
using Finance.Application.Services.Pagination.Base;

namespace Finance.Application.Services.Pagination
{
    public class TransactionParams : PagingParams
    {
        public bool? TransactionStatus { get; set; }
        public DateTime StartDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
    }
}