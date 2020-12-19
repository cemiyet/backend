using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Queries.Authors
{
    public class ListQuery : PageableModel, IRequest<List<AuthorViewModel>>
    {
    }

    public class ListQueryValidator : AbstractValidator<ListQuery>
    {
        public ListQueryValidator()
        {
            RuleFor(pm => pm.Page).GreaterThan(0);
            RuleFor(pm => pm.PageSize).GreaterThan(0);
        }
    }
}
