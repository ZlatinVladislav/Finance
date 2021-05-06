using Finance.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Finance.Application.Commands.Transaction;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Querries.Transaction;
using Microsoft.AspNetCore.Authorization;

namespace Finance.Controllers
{
    public class TransactionController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionDetails.Query {Id = id}));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            return HandleResult(await Mediator.Send(new TransactionList.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDto model)
        {
            return HandleResult(await Mediator.Send(new TransactionCreate.Command {TransactionDto = model}));
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancellTransaction(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionCancel.Command {Id = id}));
        }

        // [Authorize(Policy = "IsUser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransaction(Guid id, TransactionDto model)
        {
            return HandleResult(await Mediator.Send(new TransactionEdit.Command {TransactionDto = model}));
        }

        // [Authorize(Policy = "IsUser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionDelete.Command {Id = id}));
        }
    }
}