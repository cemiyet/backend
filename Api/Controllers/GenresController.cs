using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public IActionResult Add()
        {
            throw new NotImplementedException("TODO");
        }

        // GET {{url}}/genres?page=<page>&pageSize=<pageSize>
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> List([FromQuery] ListQuery paging)
        {
            return await _mediator.Send(paging);
        }

        // GET {{url}}/genres/<id>
        [HttpGet("{id}")]
        public IActionResult Details(Guid id)
        {
            throw new NotImplementedException("TODO");
        }

        // {{url}}/genres/<id>/books?page=<page>&pageSize=<pageSize>
        [HttpGet("{id}/books")]
        public IActionResult ListBooks(Guid id, [FromQuery] int page = 1,
            [FromQuery] int pageSize = Constants.PageSize)
        {
            throw new NotImplementedException("TODO");
        }

        // PUT {{url}}/genres/<id>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id)
        {
            throw new NotImplementedException("TODO");
        }

        // DELETE {{url}}/genres/<id>
        [HttpDelete("{id}")]
        public IActionResult DeleteOne(Guid id)
        {
            throw new NotImplementedException("TODO");
        }

        // DELETE {{url}}/genres?id=<id_1>&id=<id_n>
        [HttpDelete]
        public IActionResult DeleteMany([FromQuery] Guid[] id)
        {
            throw new NotImplementedException("TODO");
        }
    }
}
