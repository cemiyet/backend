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
using Cemiyet.Application.Genres.Queries.ListBooks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [GenresExceptionFilter]
    public class GenresController : CemiyetBaseController
    {
        public GenresController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data) => await Mediator.Send(data);

        [HttpGet]
        [ProducesResponseType(typeof(List<GenreViewModel>), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<List<GenreViewModel>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}/books")]
        [ProducesResponseType(typeof(List<BookViewModel>), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<List<BookViewModel>>> ListBooks(Guid id, [FromQuery] ListBooksQuery query)
        {
            query.Id = id;
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenreViewModel), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<GenreViewModel>> Details(Guid id) => await Mediator.Send(new DetailsQuery { Id = id });

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
        public async Task<ActionResult<Unit>> DeleteOne(Guid id) => await Mediator.Send(new DeleteOneCommand { Id = id });

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(GenreNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data) => await Mediator.Send(data);
    }
}
