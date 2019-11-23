using System;
using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Authors.Queries.ListBooks
{
    public class ListBooksQuery : PageableModel, IRequest<List<BookViewModel>>
    {
        public Guid Id { get; set; }
    }
}
