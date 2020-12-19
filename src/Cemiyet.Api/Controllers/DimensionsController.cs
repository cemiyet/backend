using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Application.Commands.Dimensions;
using Cemiyet.Application.Queries.Dimensions;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class DimensionsController : CemiyetBaseController
    {
        public DimensionsController(IMediator mediator) : base(mediator)
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
        [ProducesResponseType(typeof(List<DimensionViewModel>), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<List<DimensionViewModel>>> List([FromQuery] ListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DimensionViewModel), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<DimensionViewModel>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsQuery { Id = id });
        }

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
        public async Task<ActionResult<Unit>> DeleteOne(Guid id)
        {
            return await Mediator.Send(new DeleteOneCommand { Id = id });
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), 200)]
        [ProducesResponseType(typeof(DimensionNotFoundException), 400)]
        public async Task<ActionResult<Unit>> DeleteMany([FromBody] DeleteManyCommand data)
        {
            return await Mediator.Send(data);
        }
    }
}
