using System;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Books.Queries.DetailsEdition
{
    public class DetailsEditionQuery : IRequest<BookEditionViewModel>
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
    }
}
