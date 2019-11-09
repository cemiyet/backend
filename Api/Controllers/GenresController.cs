using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Genres.Commands.Add;
using Cemiyet.Application.Genres.Commands.DeleteMany;
using Cemiyet.Application.Genres.Commands.DeleteOne;
using Cemiyet.Application.Genres.Commands.Update;
using Cemiyet.Application.Genres.Queries.Details;
using Cemiyet.Application.Genres.Queries.List;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [GenresExceptionFilter]
    public class GenresController : CemiyetBaseController
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Genre>), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<List<Genre>>> List([FromQuery] ListQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Genre), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Genre>> Details(Guid id)
        {
            return await _mediator.Send(new DetailsQuery {Id = id});
        }

//        [HttpGet("{id}/books")]
//        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
//        public IActionResult ListBooks(Guid id, [FromQuery] ListQuery query)
//        {
//            throw new NotImplementedException("TODO (v0.3)");
//        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne(Guid id)
        {
            return await _mediator.Send(new DeleteOneCommand {Id = id});
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data)
        {
            return await _mediator.Send(data);
        }
    }
}
