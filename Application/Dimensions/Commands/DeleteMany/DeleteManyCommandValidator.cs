using FluentValidation;

namespace Cemiyet.Application.Dimensions.Commands.DeleteMany
{
    public class DeleteManyCommandValidator : AbstractValidator<DeleteManyCommand>
    {
        public DeleteManyCommandValidator()
        {
            RuleFor(dmc => dmc.Ids).NotNull();
            RuleFor(dmc => dmc.Ids.Length).GreaterThan(1);
            RuleForEach(dmc => dmc.Ids).NotEmpty().When(dmc => dmc.Ids.Length > 1);
        }
    }
}
