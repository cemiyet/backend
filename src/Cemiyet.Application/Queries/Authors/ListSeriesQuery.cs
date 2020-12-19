using System;
using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Application.Queries.Authors
{
    public class ListSeriesQuery : PageableModel, IRequest<List<SerieViewModel>>
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class ListSeriesQueryValidator : AbstractValidator<ListSeriesQuery>
    {
        public ListSeriesQueryValidator()
        {
            RuleFor(ls => ls.Id).NotEmpty();
            RuleFor(pm => pm.Page).GreaterThan(0);
            RuleFor(pm => pm.PageSize).GreaterThan(0);
        }
    }
}
