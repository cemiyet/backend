using System;
using Cemiyet.Core.Entities;
using MediatR;

namespace Cemiyet.Application.Dimensions.Queries.Details
{
    // TODO (v0.1): create validator.
    public class DetailsQuery : IRequest<Dimension>
    {
        public Guid Id { get; set; }
    }
}
