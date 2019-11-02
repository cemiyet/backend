using FluentValidation;

namespace Cemiyet.Application.Dimensions.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(uc => uc.Id).NotNull();
            RuleFor(uc => uc.Width).NotEmpty();
            RuleFor(uc => uc.Height).NotEmpty();
        }
    }
}
