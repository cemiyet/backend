using System;
using Cemiyet.Persistence.Application.ViewModels;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Queries.Books
{
    public class DetailsEditionQuery : IRequest<BookEditionViewModel>
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
    }

    public class DetailsEditionQueryValidator : AbstractValidator<DetailsEditionQuery>
    {
        public DetailsEditionQueryValidator()
        {
            RuleFor(deq => deq.Id).NotEmpty();

            RuleFor(deq => deq.Isbn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(13);
        }
    }
}
