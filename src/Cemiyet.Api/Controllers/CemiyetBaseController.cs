using Cemiyet.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public abstract class CemiyetBaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected CemiyetBaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
