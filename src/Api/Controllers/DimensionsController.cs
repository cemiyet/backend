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
        public DimensionsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<Unit>> Add([FromBody] AddCommand data) => await Mediator.Send(data);

        [HttpGet]
        [ProducesResponseType(typeof(List<Dimension>), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<List<Dimension>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dimension), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Dimension>> Details(Guid id) => await Mediator.Send(new DetailsQuery { Id = id });

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> UpdatePartially([FromRoute] Guid id,
                                                              [FromBody] UpdatePartiallyCommand data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
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
