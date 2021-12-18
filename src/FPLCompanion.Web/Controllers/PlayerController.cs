using FPLCompanion.ApplicationServices.Requests.Player.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FPLCompanion.Controllers
{
    [ApiController]
    public class PlayerController : BaseController
    {
        [HttpGet]
        [Route("player/all")]
        public async Task<IActionResult> Create()
        {
            return this.Ok(await this.Mediator.Send(new GetAllPlayerDataQuery()));
        }
    }
}
