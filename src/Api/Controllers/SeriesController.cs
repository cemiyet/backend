using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Series.Commands.Add;
using Cemiyet.Application.Series.Commands.AddBook;
using Cemiyet.Application.Series.Queries.List;
using Cemiyet.Application.Series.Queries.Details;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [SeriesExceptionFilter]
    public class SeriesController : CemiyetBaseController
    {
        public SeriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data) => await Mediator.Send(data);

        [HttpPost("{id}/books")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> AddBook([FromRoute] Guid id, [FromBody] AddBookCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SerieViewModel>), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<List<SerieViewModel>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SerieViewModel), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<SerieViewModel>> Details([FromRoute] DetailsQuery query) => await Mediator.Send(query);
    }
}
