using System.Linq;
using FluentValidation;

namespace Cemiyet.Application.Genres.Commands.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(ShouldNotContainDigits)
                .WithMessage("Name alanı sayısal karakter içermemeli.")
                .MaximumLength(50);
        }

        private bool ShouldNotContainDigits(string s)
        {
            return !s.Any(char.IsDigit);
        }
    }
}
