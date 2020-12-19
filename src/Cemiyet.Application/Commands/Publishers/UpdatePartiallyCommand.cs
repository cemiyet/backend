using System;
using System.Linq;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Publishers
{
    public class UpdatePartiallyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePartiallyCommandValidator : AbstractValidator<UpdatePartiallyCommand>
    {
        public UpdatePartiallyCommandValidator()
        {
            RuleFor(upc => upc.Id).NotNull();

            RuleFor(upc => upc.Name).NotEmpty().When(upc => string.IsNullOrEmpty(upc.Description));

            RuleFor(upc => upc.Name)
                .Must(ShouldNotContainDigits).WithMessage("Name alanı sayısal karakter içermemeli.")
                                             .When(upc => !string.IsNullOrEmpty(upc.Name));

            RuleFor(upc => upc.Name).MaximumLength(100);

            RuleFor(upc => upc.Description).NotEmpty().When(upc => string.IsNullOrEmpty(upc.Name));

            RuleFor(upc => upc.Description).MaximumLength(2000);
        }

        private bool ShouldNotContainDigits(string s)
        {
            return !s.Any(char.IsDigit);
        }
    }
}
