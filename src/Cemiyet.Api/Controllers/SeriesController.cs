using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Application.Commands.Series;
using Cemiyet.Application.Queries.Series;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class SeriesController : CemiyetBaseController
    {
        public SeriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data)
        {
            return await Mediator.Send(data);
        }

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
        public async Task<ActionResult<List<SerieViewModel>>> List([FromQuery] ListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SerieViewModel), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<SerieViewModel>> Details([FromRoute] DetailsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartially([FromRoute] Guid id,
                                                              [FromBody] UpdatePartiallyCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne([FromRoute] DeleteOneCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}/books")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteBook([FromRoute] Guid id, [FromBody] DeleteBookCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }
    }
}
