using System;
using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Application.Queries.Authors
{
    public class ListBooksQuery : PageableModel, IRequest<List<BookViewModel>>
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class ListBooksQueryValidator : AbstractValidator<ListBooksQuery>
    {
        public ListBooksQueryValidator()
        {
            RuleFor(lb => lb.Id).NotEmpty();
            RuleFor(pm => pm.Page).GreaterThan(0);
            RuleFor(pm => pm.PageSize).GreaterThan(0);
        }
    }
}
