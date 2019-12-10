using FluentValidation;

namespace Cemiyet.Application.Series.Commands.UpdatePartially
{
    public class UpdatePartiallyCommandValidator : AbstractValidator<UpdatePartiallyCommand>
    {
        public UpdatePartiallyCommandValidator()
        {
            RuleFor(upc => upc.Id).NotNull();

            RuleFor(upc => upc.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(100)
                .NotEmpty().When(upc => string.IsNullOrEmpty(upc.Description));

            RuleFor(upc => upc.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(2000)
                .NotEmpty().When(upc => string.IsNullOrEmpty(upc.Title));
        }
    }
}
