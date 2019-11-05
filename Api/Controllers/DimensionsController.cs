using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Dimensions.Commands.Add;
using Cemiyet.Application.Dimensions.Commands.DeleteMany;
using Cemiyet.Application.Dimensions.Commands.DeleteOne;
using Cemiyet.Application.Dimensions.Commands.Update;
using Cemiyet.Application.Dimensions.Commands.UpdatePartially;
using Cemiyet.Application.Dimensions.Queries.Details;
using Cemiyet.Application.Dimensions.Queries.List;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [DimensionsExceptionFilter]
    public class DimensionsController : CemiyetBaseController
    {
        private readonly IMediator _mediator;

        public DimensionsController(IMediator mediator)
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
        [ProducesResponseType(typeof(List<Dimension>), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<List<Dimension>>> List([FromQuery] ListQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dimension), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Dimension>> Details(Guid id)
        {
            return await _mediator.Send(new DetailsQuery {Id = id});
        }

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartially([FromRoute] Guid id,
                                                              [FromBody] UpdatePartiallyCommand data)
        {
            data.Id = id;
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> Update([FromRoute] Guid id, [FromBody] UpdateCommand data)
        {
            data.Id = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteOne(Guid id)
        {
            return await _mediator.Send(new DeleteOneCommand {Id = id});
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data)
        {
            return await _mediator.Send(data);
        }
    }
}
