using System;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Authors.Queries.Details
{
    public class DetailsQuery : IRequest<AuthorViewModel>
    {
        public Guid Id { get; set; }
    }
}
