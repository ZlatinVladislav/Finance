using System;
using System.Threading.Tasks;
using Finance.Application.Commands.BankCommands;
using Finance.Application.Commands.TransactionCommands;
using Finance.Application.DtoModels.Bank;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Querries.Bank;
using Finance.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    public class BankController: BaseApiController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllBanks([FromQuery] PagingParams param)
        {
            return HandleResult(await Mediator.Send(new BankList.Query {Params = param}));
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction(BankDto model)
        {
            return HandleResult(await Mediator.Send(new BankAdd.Command {BankDto = model}));
        }
        
        [AllowAnonymous]
        [HttpPost("{bankId}/transaction/{transactionId}")]
        public async Task<IActionResult> AddTransactionToBank(Guid bankId,Guid transactionId)
        {
            return HandleResult(await Mediator.Send(new BankTransactionAdd.Command { BankId = bankId,TransactionId = transactionId}));
        }
    }
}