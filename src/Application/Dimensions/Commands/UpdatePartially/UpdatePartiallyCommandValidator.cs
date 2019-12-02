using FluentValidation;

namespace Cemiyet.Application.Dimensions.Commands.UpdatePartially
{
    public class UpdatePartiallyCommandValidator : AbstractValidator<UpdatePartiallyCommand>
    {
        public UpdatePartiallyCommandValidator()
        {
            RuleFor(upc => upc.Id).NotNull();

            RuleFor(upc => upc.Width)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().When(upc => upc.Height.Equals(default))
                .GreaterThan(1).When(upc => !upc.Width.Equals(default));

            RuleFor(upc => upc.Height)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().When(upc => upc.Width.Equals(default))
                .GreaterThan(1).When(upc => !upc.Height.Equals(default));
        }
    }
}
