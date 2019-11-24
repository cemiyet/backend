using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Books.Queries.List;
using Cemiyet.Application.Books.Queries.Details;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Api.Controllers
{
    [BooksExceptionFilter]
    public class BooksController : CemiyetBaseController
    {
        public BooksController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookViewModel>), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<List<BookViewModel>>> List([FromQuery] ListQuery query) => await Mediator.Send(query);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookViewModel), 200)]
        [ProducesResponseType(typeof(BookNotFoundException), 400)]
        public async Task<ActionResult<BookViewModel>> Details(Guid id) => await Mediator.Send(new DetailsQuery {Id = id});
    }
}
