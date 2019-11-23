using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Authors.Commands.Add;
using Cemiyet.Application.Authors.Commands.UpdatePartially;
using Cemiyet.Application.Authors.Commands.Update;
using Cemiyet.Application.Authors.Commands.DeleteOne;
using Cemiyet.Application.Authors.Commands.DeleteMany;
using Cemiyet.Application.Authors.Queries.Details;
using Cemiyet.Application.Authors.Queries.List;
using Cemiyet.Application.Authors.Queries.ListBooks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [AuthorsExceptionFilter]
    public class AuthorsController : CemiyetBaseController
    {
        public AuthorsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data) => await Mediator.Send(data);

        [HttpGet]
        [ProducesResponseType(typeof(List<AuthorViewModel>), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<List<AuthorViewModel>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AuthorViewModel), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<AuthorViewModel>> Details(Guid id) => await Mediator.Send(new DetailsQuery { Id = id });

        [HttpGet("{id}/books")]
        [ProducesResponseType(typeof(List<BookViewModel>), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<List<BookViewModel>>> ListBooks(Guid id, [FromQuery] ListBooksQuery query)
        {
            query.Id = id;
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
        public async Task<ActionResult<Unit>> DeleteOne(Guid id) => await Mediator.Send(new DeleteOneCommand { Id = id });

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data) => await Mediator.Send(data);
    }
}
