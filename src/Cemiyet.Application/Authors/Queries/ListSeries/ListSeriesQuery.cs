using System;
using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Application.Authors.Queries.ListSeries
{
    public class ListSeriesQuery : PageableModel, IRequest<List<SerieViewModel>>
    {
        [FromRoute]
        public Guid Id { get; set; }
    }
}
