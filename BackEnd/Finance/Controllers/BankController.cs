using System;
using System.Threading.Tasks;
using Finance.Application.CQRS.Commands.BankCommands;
using Finance.Application.CQRS.Querries.Bank;
using Finance.Application.DtoModels.Bank;
using Finance.Application.Services.Pagination.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    public class BankController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBanks()
        {
            return HandleResult(await Mediator.Send(new BankList.Query {}));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddTransaction(BankDto model)
        {
            return HandleResult(await Mediator.Send(new BankAdd.Command {BankDto = model}));
        }
        
        [HttpPost("{bankId}/transaction/{transactionId}")]
        public async Task<IActionResult> AddTransactionToBank(Guid bankId, Guid transactionId)
        {
            return HandleResult(await Mediator.Send(new BankTransactionAdd.Command
                {BankId = bankId, TransactionId = transactionId}));
        }
    }
}