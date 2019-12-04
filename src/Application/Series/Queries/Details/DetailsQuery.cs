using System;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Series.Queries.Details
{
    public class DetailsQuery : IRequest<SerieViewModel>
    {
        public Guid Id { get; set; }
    }
}
