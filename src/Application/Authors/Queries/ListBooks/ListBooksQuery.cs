using System;
using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Application.Authors.Queries.ListBooks
{
    public class ListBooksQuery : PageableModel, IRequest<List<BookViewModel>>
    {
        [FromRoute]
        public Guid Id { get; set; }
    }
}
