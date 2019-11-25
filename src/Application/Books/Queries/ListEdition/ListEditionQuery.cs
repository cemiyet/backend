using System;
using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Books.Queries.ListEdition
{
    public class ListEditionQuery : PageableModel, IRequest<List<BookEditionViewModel>>
    {
        public Guid Id { get; set; }
    }
}
