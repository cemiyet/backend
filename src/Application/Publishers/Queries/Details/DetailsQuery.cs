using System;
using Cemiyet.Core.Entities;
using MediatR;

namespace Cemiyet.Application.Publishers.Queries.Details
{
    public class DetailsQuery : IRequest<Publisher>
    {
        public Guid Id { get; set; }
    }
}
