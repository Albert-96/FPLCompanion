using FPLCompanion.ApplicationServices.Requests.Fixture.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FPLCompanion.Controllers
{
    [ApiController]
    public class FixtureController : BaseController
    {
        [HttpGet]
        [Route("fixture/event/{id}")]
        public async Task<IActionResult> GetEventFixtures(int id)
        {
            return this.Ok(await this.Mediator.Send(new GetEventFixtureQuery { eventId = id }));
        }
    }
}
