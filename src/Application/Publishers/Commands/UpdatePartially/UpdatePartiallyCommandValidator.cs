using System.Linq;
using FluentValidation;

namespace Cemiyet.Application.Publishers.Commands.UpdatePartially
{
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
