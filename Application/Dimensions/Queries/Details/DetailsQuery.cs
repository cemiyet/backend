using System;
using Cemiyet.Core.Entities;
using MediatR;

namespace Cemiyet.Application.Dimensions.Queries.Details
{
    public class DetailsQuery : IRequest<Dimension>
    {
        public Guid Id { get; set; }
    }
}
