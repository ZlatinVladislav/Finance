using System.Threading.Tasks;
using Finance.Application.CQRS.Commands.UserCommands;
using Finance.Application.CQRS.Querries.User;
using Finance.Application.DtoModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    public class UserProfileController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetTransactionById(string userName)
        {
            return HandleResult(await Mediator.Send(new UserDetails.Query {UserName = userName}));
        }
        
        [HttpPost]
        public async Task<IActionResult> PostDescription(UserDescriptionDto model)
        {
            return HandleResult(await Mediator.Send(new UserDescriptionCreate.Command {UserDescriptionDto = model}));
        }
        
        [HttpPut]
        public async Task<IActionResult> PutDescription(UserDescriptionDto model)
        {
            return HandleResult(await Mediator.Send(new UserDescriptionUpdate.Command {UserDescriptionDto = model}));
        }
    }
}