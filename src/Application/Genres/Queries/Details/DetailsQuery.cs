using System;
using Cemiyet.Core.Entities;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.Details
{
    public class DetailsQuery : IRequest<Genre>
    {
        public Guid Id { get; set; }
    }
}
