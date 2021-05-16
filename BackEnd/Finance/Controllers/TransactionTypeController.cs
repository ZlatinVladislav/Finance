using System;
using System.Threading.Tasks;
using Finance.Application.Commands.TransactionTypeCommands;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.Querries.Transaction;
using Finance.Application.Querries.TransactionType;
using Finance.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    public class TransactionTypeController: BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] TransactionTypeParams param)
        {
            return HandlePageResult(await Mediator.Send(new TransactionTypeList.Query{Params = param}));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionTypeDetails.Query {Id = id}));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionTypeDto model)
        {
            return HandleResult(await Mediator.Send(new TransactionTypeCreate.Command {TransactionTypeDto = model}));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransaction(Guid id, TransactionTypeDto model)
        {
            model.Id = id;
            return HandleResult(await Mediator.Send(new TransactionTypeEdit.Command {TransactionTypeDto = model}));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionTypeDelete.Command {Id = id}));
        }
    }
}