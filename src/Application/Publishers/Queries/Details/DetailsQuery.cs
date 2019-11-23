using System;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Publishers.Queries.Details
{
    public class DetailsQuery : IRequest<PublisherViewModel>
    {
        public Guid Id { get; set; }
    }
}
