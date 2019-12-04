using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Series.Queries.List;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [SeriesExceptionFilter]
    public class SeriesController : CemiyetBaseController
    {
        public SeriesController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<SerieViewModel>), 200)]
        [ProducesResponseType(typeof(SerieNotFoundException), 400)]
        public async Task<ActionResult<List<SerieViewModel>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);
    }
}