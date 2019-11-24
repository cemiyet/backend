using System;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Books.Queries.Details
{
    public class DetailsQuery : IRequest<BookViewModel>
    {
        public Guid Id { get; set; }
    }
}
