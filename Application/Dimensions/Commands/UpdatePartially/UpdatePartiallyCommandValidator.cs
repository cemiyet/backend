using FluentValidation;

namespace Cemiyet.Application.Dimensions.Commands.UpdatePartially
{
    public class UpdatePartiallyCommandValidator : AbstractValidator<UpdatePartiallyCommand>
    {
        public UpdatePartiallyCommandValidator()
        {
            RuleFor(upc => upc.Id).NotNull();
            RuleFor(upc => upc.Width).NotEmpty().When(upc => upc.Height.Equals(default));
            RuleFor(upc => upc.Height).NotEmpty().When(upc => upc.Width.Equals(default));
        }
    }
}
