using FluentValidation;

namespace Cemiyet.Application.Series.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(uc => uc.Id).NotNull();

            RuleFor(uc => uc.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(uc => uc.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
