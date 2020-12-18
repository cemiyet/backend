using FluentValidation;

namespace Cemiyet.Application.Dimensions.Commands.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Width)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);

            RuleFor(ac => ac.Height)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);
        }
    }
}
