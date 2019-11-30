using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Books.Commands.Add;
using Cemiyet.Application.Books.Commands.AddEdition;
using Cemiyet.Application.Books.Commands.Update;
using Cemiyet.Application.Books.Commands.UpdateEdition;
using Cemiyet.Application.Books.Commands.UpdatePartially;
using Cemiyet.Application.Books.Commands.UpdatePartiallyEdition;
using Cemiyet.Application.Books.Commands.DeleteOne;
using Cemiyet.Application.Books.Commands.DeleteOneEdition;
using Cemiyet.Application.Books.Commands.DeleteMany;
using Cemiyet.Application.Books.Commands.DeleteManyEdition;
using Cemiyet.Application.Books.Queries.List;
using Cemiyet.Application.Books.Queries.ListEdition;
using Cemiyet.Application.Books.Queries.Details;
using Cemiyet.Application.Books.Queries.DetailsEdition;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [BooksExceptionFilter]
    public class BooksController : CemiyetBaseController
    {
        public BooksController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data) => await Mediator.Send(data);

        [HttpPost("{id}/editions")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> AddEdition([FromRoute] Guid id, [FromBody] AddEditionCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPut("{id}/editions/{isbn}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdateEdition([FromRoute] Guid id,
                                                            [FromRoute] string isbn,
                                                            [FromBody] UpdateEditionCommand data)
        {
            data.Id = id;
            data.Isbn = isbn;
            return await Mediator.Send(data);
        }

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartially([FromRoute] Guid id,
                                                              [FromBody] UpdatePartiallyCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPatch("{id}/editions/{isbn}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartiallyEdition([FromRoute] Guid id,
                                                                     [FromRoute] string isbn,
                                                                     [FromBody] UpdatePartiallyEditionCommand data)
        {
            data.Id = id;
            data.Isbn = isbn;
            return await Mediator.Send(data);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookViewModel>), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<List<BookViewModel>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}/editions")]
        [ProducesResponseType(typeof(List<BookEditionViewModel>), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<List<BookEditionViewModel>>> ListEdition([FromRoute] Guid id,
                                                                                [FromQuery] ListEditionQuery query)
        {
            query.Id = id;
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookViewModel), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<BookViewModel>> Details(Guid id) => await Mediator.Send(new DetailsQuery { Id = id });

        [HttpGet("{id}/editions/{isbn}")]
        [ProducesResponseType(typeof(BookEditionViewModel), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<BookEditionViewModel>> DetailsEdition([FromRoute] DetailsEditionQuery query) => await Mediator.Send(query);

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne(Guid id) => await Mediator.Send(new DeleteOneCommand { Id = id });

        [HttpDelete("{id}/editions/{isbn}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOneEdition([FromRoute] DeleteOneEditionCommand command) => await Mediator.Send(command);

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data) => await Mediator.Send(data);

        [HttpDelete("{id}/editions")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyEditionCommand data) => await Mediator.Send(data);
    }
}
