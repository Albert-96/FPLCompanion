using FPLCompanion.ApplicationServices.Requests.DreamTeamWeek.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FPLCompanion.Controllers
{
    [ApiController]
    public class DreamTeamController : BaseController
    {
        [HttpGet]
        [Route("deamteam/events")]
        public async Task<IActionResult> GetDreamTeamEvents()
        {
            return this.Ok(await this.Mediator.Send(new GetDreamTeamEventsQuery()));
        }

        [HttpGet]
        [Route("deamteam/{id}")]
        public async Task<IActionResult> GetDreamTeam(int id)
        {
            return this.Ok(await this.Mediator.Send(new GetDreamTeamQuery { id = id}));
        }
    }
}
