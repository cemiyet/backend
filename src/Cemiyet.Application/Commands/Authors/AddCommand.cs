using System.Linq;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Authors
{
    public class AddCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
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
                .MaximumLength(25);

            RuleFor(ac => ac.Surname)
                .Cascade(CascadeMode.Stop)
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
