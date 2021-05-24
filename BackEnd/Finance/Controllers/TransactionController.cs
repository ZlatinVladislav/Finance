using System;
using System.Threading.Tasks;
using Finance.Application.CQRS.Commands.TransactionCommands;
using Finance.Application.CQRS.Querries.Transaction;
using Finance.Application.DtoModels.Transaction;
using Finance.Application.Services.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    public class TransactionController : BaseApiController
    {
        [Authorize(Policy = "TransactionByIdRequirement")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionDetails.Query {Id = id}));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] TransactionParams param)
        {
            return HandlePageResult(await Mediator.Send(new TransactionList.Query {Params = param}));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDto model)
        {
            return HandleResult(await Mediator.Send(new TransactionCreate.Command {TransactionDto = model}));
        }

        [Authorize(Policy = "TransactionByIdRequirement")]
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancellTransaction(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionCancel.Command {Id = id}));
        }

        [Authorize(Policy = "TransactionByIdRequirement")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransaction(Guid id, TransactionDto model)
        {
            model.Id = id;
            return HandleResult(await Mediator.Send(new TransactionEdit.Command {TransactionDto = model}));
        }

        [Authorize(Policy = "TransactionByIdRequirement")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionDelete.Command {Id = id}));
        }
    }
}