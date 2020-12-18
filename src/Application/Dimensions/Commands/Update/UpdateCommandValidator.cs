using FluentValidation;

namespace Cemiyet.Application.Dimensions.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(uc => uc.Id).NotNull();

            RuleFor(uc => uc.Width)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);

            RuleFor(uc => uc.Height)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);
        }
    }
}
