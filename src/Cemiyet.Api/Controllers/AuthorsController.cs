using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Application.Commands.Authors;
using Cemiyet.Application.Queries.Authors;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class AuthorsController : CemiyetBaseController
    {
        public AuthorsController(IMediator mediator) : base(mediator)
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

        [HttpGet]
        [ProducesResponseType(typeof(List<AuthorViewModel>), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<List<AuthorViewModel>>> List([FromQuery] ListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}/books")]
        [ProducesResponseType(typeof(List<BookViewModel>), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<List<BookViewModel>>> ListBooks([FromQuery] ListBooksQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}/series")]
        [ProducesResponseType(typeof(List<SerieViewModel>), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<List<SerieViewModel>>> ListSeries([FromQuery] ListSeriesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AuthorViewModel), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<AuthorViewModel>> Details([FromRoute] DetailsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartially([FromRoute] Guid id,
                                                              [FromBody] UpdatePartiallyCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne([FromRoute] DeleteOneCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data)
        {
            return await Mediator.Send(data);
        }
    }
}
