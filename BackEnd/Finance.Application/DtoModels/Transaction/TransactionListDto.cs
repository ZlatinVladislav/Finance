using System.Collections.Generic;
using Finance.Application.DtoModels.Base;

namespace Finance.Application.DtoModels.Transaction
{
    public class TransactionListDto : BaseDto
    {
        public IEnumerable<TransactionGetDto> Transactions { get; set; }
    }
}
