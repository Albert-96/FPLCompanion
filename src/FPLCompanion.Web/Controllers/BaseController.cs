using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FPLCompanion.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Logger.
        /// </summary>
        protected ILogger logger;

        /// <summary>
        /// Mediator.
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Mediator implementation.
        /// </summary>
        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}
