using FluentValidation;

namespace Cemiyet.Application.Series.Commands.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(ac => ac.Description).MaximumLength(2000);
        }
    }
}
