using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cemiyet.Application.Authors.Queries.List;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    public class AuthorsController : CemiyetBaseController
    {
        public AuthorsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Author>), 200)]
        [ProducesResponseType(typeof(AuthorNotFoundException), 400)]
        public async Task<ActionResult<List<Author>>> List([FromQuery] ListQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
