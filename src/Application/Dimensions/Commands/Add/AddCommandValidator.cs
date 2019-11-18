using FluentValidation;

namespace Cemiyet.Application.Dimensions.Commands.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Width).NotEmpty();
            RuleFor(ac => ac.Height).NotEmpty();
        }
    }
}
