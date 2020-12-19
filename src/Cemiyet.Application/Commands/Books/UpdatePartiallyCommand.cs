using System;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Books
{
    public class UpdatePartiallyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePartiallyCommandValidator : AbstractValidator<UpdatePartiallyCommand>
    {
        public UpdatePartiallyCommandValidator()
        {
            RuleFor(upc => upc.Id).NotNull();

            RuleFor(upc => upc.Title).NotEmpty().When(upc => string.IsNullOrEmpty(upc.Description));
            RuleFor(upc => upc.Title).MaximumLength(100);

            RuleFor(upc => upc.Description).NotEmpty().When(upc => string.IsNullOrEmpty(upc.Title));
            RuleFor(upc => upc.Description).MaximumLength(2500);
        }
    }
}
