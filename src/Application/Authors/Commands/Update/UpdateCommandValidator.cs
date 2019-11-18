using System.Linq;
using FluentValidation;

namespace Cemiyet.Application.Authors.Commands.Update
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
                .MaximumLength(25);

            RuleFor(uc => uc.Surname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ShouldNotContainDigits)
                .WithMessage("Surname alanı sayısal karakter içermemeli.")
                .MaximumLength(25);

            RuleFor(uc => uc.Bio)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(2000);
        }

        private bool ShouldNotContainDigits(string s)
        {
            return !s.Any(char.IsDigit);
        }
    }
}
