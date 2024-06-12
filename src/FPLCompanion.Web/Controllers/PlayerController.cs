using FPLCompanion.ApplicationServices.Requests.Player.Queries;
using FPLCompanion.Dependencies;
using FPLCompanion.Dto;
using Microsoft.AspNetCore.Mvc;
using PrimeNGTableExtension.Models;

namespace FPLCompanion.Controllers
{
    [ApiController]
    public class PlayerController : BaseController
    {
        [HttpPost]
        [Route("player/all")]
        public async Task<IActionResult> GetAllPlayers(TableRequestModel gridParams)
        {
            return this.Ok(await this.Mediator.Send(new GetAllPlayerDataQuery { gridParams = gridParams}));
        }

        //[HttpGet]
        //[Route("player/defenders")]
        //public async Task<IActionResult> GetDefenders(DataSourceLoadOptions loadOptions)
        //{
        //    return this.Ok(await this.Mediator.Send(new GetDefenderDataQuery { loadOptions = loadOptions }));
        //}

        //[HttpGet]
        //[Route("player/midfielder")]
        //public async Task<IActionResult> GetMidfielders(DataSourceLoadOptions loadOptions)
        //{
        //    return this.Ok(await this.Mediator.Send(new GetMidfielderDataQuery { loadOptions = loadOptions }));
        //}

        //[HttpGet]
        //[Route("player/forward")]
        //public async Task<IActionResult> GetForwards(DataSourceLoadOptions loadOptions)
        //{
        //    return this.Ok(await this.Mediator.Send(new GetForwardDataQuery { loadOptions = loadOptions }));
        //}

        //[HttpGet]
        //[Route("player/goalkeeper")]
        //public async Task<IActionResult> GetGoalkeepers(DataSourceLoadOptions loadOptions)
        //{
        //    return this.Ok(await this.Mediator.Send(new GetGoalkeeperDataQuery { loadOptions = loadOptions }));
        //}
    }
}
