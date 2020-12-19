using System.Linq;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Genres
{
    public class AddCommand : IRequest
    {
        public string Name { get; set; }
    }

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
