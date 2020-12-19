using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Application.Commands.Genres;
using Cemiyet.Application.Queries.Genres;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class GenresController : CemiyetBaseController
    {
        public GenresController(IMediator mediator) : base(mediator)
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
        [ProducesResponseType(typeof(List<GenreViewModel>), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<List<GenreViewModel>>> List([FromQuery] ListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}/books")]
        [ProducesResponseType(typeof(List<BookViewModel>), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<List<BookViewModel>>> ListBooks([FromQuery] ListBooksQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenreViewModel), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<GenreViewModel>> Details([FromRoute] DetailsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne([FromRoute] DeleteOneCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data)
        {
            return await Mediator.Send(data).ConfigureAwait(false);
        }
    }
}
