using System.Linq;
using FluentValidation;

namespace Cemiyet.Application.Authors.Commands.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ShouldNotContainDigits)
                .WithMessage("Name alanı sayısal karakter içermemeli.")
                .MaximumLength(25);

            RuleFor(ac => ac.Surname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ShouldNotContainDigits)
                .WithMessage("Surname alanı sayısal karakter içermemeli.")
                .MaximumLength(25);

            RuleFor(ac => ac.Bio).MaximumLength(2000);
        }

        private bool ShouldNotContainDigits(string s)
        {
            return !s.Any(char.IsDigit);
        }
    }
}
