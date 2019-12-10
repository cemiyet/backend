using FluentValidation;

namespace Cemiyet.Application.Series.Commands.UpdatePartially
{
    public class UpdatePartiallyCommandValidator : AbstractValidator<UpdatePartiallyCommand>
    {
        public UpdatePartiallyCommandValidator()
        {
            RuleFor(upc => upc.Id).NotNull();

            RuleFor(upc => upc.Title).NotEmpty().When(upc => string.IsNullOrEmpty(upc.Description));
            RuleFor(upc => upc.Title).MaximumLength(100);

            RuleFor(upc => upc.Description).NotEmpty().When(upc => string.IsNullOrEmpty(upc.Title));
            RuleFor(upc => upc.Description).MaximumLength(2000);
        }
    }
}
