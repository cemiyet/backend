using System;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Dimensions.Queries.Details
{
    public class DetailsQuery : IRequest<DimensionViewModel>
    {
        public Guid Id { get; set; }
    }
}
