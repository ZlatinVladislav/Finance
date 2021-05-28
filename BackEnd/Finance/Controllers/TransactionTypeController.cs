using System;
using System.Threading.Tasks;
using Finance.Application.CQRS.Commands.TransactionTypeCommands;
using Finance.Application.CQRS.Querries.TransactionType;
using Finance.Application.DtoModels.TransactionType;
using Finance.Application.Services.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    public class TransactionTypeController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsTypePaged([FromQuery] TransactionTypeParams param)
        {
            return HandlePageResult(await Mediator.Send(new TransactionTypeList.Query {Params = param}));
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTransactionsType()
        {
            return HandleResult(await Mediator.Send(new TransactionTypeListAll.Query{}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionTypeById(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionTypeDetails.Query {Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> AddTransactionType(TransactionTypeDto model)
        {
            return HandleResult(await Mediator.Send(new TransactionTypeCreate.Command {TransactionTypeDto = model}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransactionType(Guid id, TransactionTypeDto model)
        {
            model.Id = id;
            return HandleResult(await Mediator.Send(new TransactionTypeEdit.Command {TransactionTypeDto = model}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionType(Guid id)
        {
            return HandleResult(await Mediator.Send(new TransactionTypeDelete.Command {Id = id}));
        }
    }
}