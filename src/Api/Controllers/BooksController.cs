using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cemiyet.Api.Filters;
using Cemiyet.Application.Books.Queries.List;
using Cemiyet.Application.Books.Queries.ListEdition;
using Cemiyet.Application.Books.Queries.Details;
using Cemiyet.Application.Books.Queries.DetailsEdition;
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
        public async Task<ActionResult<BookViewModel>> Details(Guid id) => await Mediator.Send(new DetailsQuery { Id = id });

        [HttpGet("{id}/editions")]
        [ProducesResponseType(typeof(List<BookEditionViewModel>), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<List<BookEditionViewModel>>> ListEdition([FromRoute] Guid id,
                                                                                [FromQuery] ListEditionQuery query)
        {
            query.Id = id;
            return await Mediator.Send(query);
        }

        [HttpGet("{id}/editions/{isbn}")]
        [ProducesResponseType(typeof(BookEditionViewModel), 200)]
        [ProducesResponseType(typeof(BookEditionNotFoundException), 400)]
        public async Task<ActionResult<BookEditionViewModel>> DetailsEdition([FromRoute] DetailsEditionQuery query) => await Mediator.Send(query);
    }
}
