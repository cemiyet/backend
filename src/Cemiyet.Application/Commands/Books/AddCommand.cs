using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Books
{
    public class AddCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Guid> GenreIds { get; set; }
        public ICollection<Guid> AuthorIds { get; set; }
    }

    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(ac => ac.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(2500);

            RuleFor(ac => ac.GenreIds).NotEmpty();
            RuleFor(ac => ac.AuthorIds).NotEmpty();
        }
    }
}
