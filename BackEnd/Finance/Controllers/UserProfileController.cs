using System.Threading.Tasks;
using Finance.Application.Querries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    public class UserProfileController:BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetTransactionById(string userName)
        {
            return HandleResult(await Mediator.Send(new UserDetails.Query {UserName = userName}));
        }
    }
}