using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Application.Publishers.Commands.Add;
using Cemiyet.Application.Publishers.Commands.UpdatePartially;
using Cemiyet.Application.Publishers.Commands.Update;
using Cemiyet.Application.Publishers.Commands.DeleteOne;
using Cemiyet.Application.Publishers.Commands.DeleteMany;
using Cemiyet.Application.Publishers.Queries.List;
using Cemiyet.Application.Publishers.Queries.Details;
using Cemiyet.Application.Publishers.Queries.ListBooks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class PublishersController : CemiyetBaseController
    {
        public PublishersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data) => await Mediator.Send(data);

        [HttpGet]
        [ProducesResponseType(typeof(List<PublisherViewModel>), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<List<PublisherViewModel>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}/books")]
        [ProducesResponseType(typeof(List<BookEditionViewModel>), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<List<BookEditionViewModel>>> ListBooks([FromQuery] ListBooksQuery query) => await Mediator.Send(query);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublisherViewModel), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<PublisherViewModel>> Details([FromRoute] DetailsQuery query) => await Mediator.Send(query);

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartially([FromRoute] Guid id,
                                                              [FromBody] UpdatePartiallyCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne([FromRoute] DeleteOneCommand command) => await Mediator.Send(command);

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(PublisherNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data) => await Mediator.Send(data);
    }
}
