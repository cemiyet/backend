using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cemiyet.Application.Genres.Commands.Add;
using Cemiyet.Application.Genres.Commands.DeleteOne;
using Cemiyet.Application.Genres.Commands.Update;
using Cemiyet.Application.Genres.Queries.Details;
using Cemiyet.Application.Genres.Queries.List;
using Cemiyet.Core;
using Cemiyet.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class GenresController : CemiyetBaseController
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST {{url}}/genres
        [HttpPost]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data)
        {
            return await _mediator.Send(data);
        }

        // GET {{url}}/genres?page=<page>&pageSize=<pageSize>
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> List([FromQuery] ListQuery paging)
        {
            return await _mediator.Send(paging);
        }

        // GET {{url}}/genres/<id>
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Details(Guid id)
        {
            return await _mediator.Send(new DetailsQuery {Id = id});
        }

        // {{url}}/genres/<id>/books?page=<page>&pageSize=<pageSize>
        [HttpGet("{id}/books")]
        public IActionResult ListBooks(Guid id, [FromQuery] int page = 1,
            [FromQuery] int pageSize = Constants.PageSize)
        {
            throw new NotImplementedException("TODO @ v0.3");
        }

        // PUT {{url}}/genres/<id>
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id,
            [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await _mediator.Send(data);
        }

        // DELETE {{url}}/genres/<id>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteOne(Guid id)
        {
            return await _mediator.Send(new DeleteOneCommand {Id = id});
        }

        // DELETE {{url}}/genres?id=<id_1>&id=<id_n>
        [HttpDelete]
        public IActionResult DeleteMany([FromQuery] Guid[] id)
        {
            throw new NotImplementedException("TODO @ v0.1");
        }
    }
}
