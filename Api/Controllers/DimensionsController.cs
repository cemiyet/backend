using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cemiyet.Application.Dimensions.Commands.Add;
using Cemiyet.Application.Dimensions.Queries.Details;
using Cemiyet.Application.Dimensions.Queries.List;
using Cemiyet.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class DimensionsController : CemiyetBaseController
    {
        private readonly IMediator _mediator;

        public DimensionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST {{url}}/dimensions
        [HttpPost]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data)
        {
            return await _mediator.Send(data);
        }

        // GET {{url}}/dimensions?page=<page>&pageSize=<pageSize>
        [HttpGet]
        public async Task<ActionResult<List<Dimension>>> List([FromQuery] ListQuery query)
        {
            return await _mediator.Send(query);
        }

        // GET {{url}}/dimensions/<id>
        [HttpGet("{id}")]
        public async Task<ActionResult<Dimension>> Details(Guid id)
        {
            return await _mediator.Send(new DetailsQuery {Id = id});
        }
    }
}
