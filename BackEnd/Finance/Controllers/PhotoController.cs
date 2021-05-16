using System.Threading.Tasks;
using Finance.Application.Commands.PhotoCommands;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.Photos;

namespace Finance.Controllers
{
    public class PhotoController:BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> AddPhoto([FromForm] PhotoAdd.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }
        
        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(string id)
        {
            return HandleResult(await Mediator.Send(new PhotoSetMain.Command{Id = id}));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(string id)
        {
            return HandleResult(await Mediator.Send(new PhotoDelete.Command{Id = id}));
        }
    }
}