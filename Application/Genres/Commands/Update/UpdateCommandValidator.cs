using System.Linq;
using FluentValidation;

namespace Cemiyet.Application.Genres.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(uc => uc.Id).NotNull();
            RuleFor(uc => uc.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
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
