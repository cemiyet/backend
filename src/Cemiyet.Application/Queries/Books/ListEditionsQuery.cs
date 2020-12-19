using System;
using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Queries.Books
{
    public class ListEditionsQuery : PageableModel, IRequest<List<BookEditionViewModel>>
    {
        public Guid Id { get; set; }
    }

    public class ListEditionQueryValidator : AbstractValidator<ListEditionsQuery>
    {
        public ListEditionQueryValidator()
        {
            RuleFor(leq => leq.Id).NotNull();
            RuleFor(pm => pm.Page).GreaterThan(0);
            RuleFor(pm => pm.PageSize).GreaterThan(0);
        }
    }
}
