using System;
using Cemiyet.Core.Entities;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.Details
{
    // TODO (v0.1): create validator.
    public class DetailsQuery : IRequest<Genre>
    {
        public Guid Id { get; set; }
    }
}
