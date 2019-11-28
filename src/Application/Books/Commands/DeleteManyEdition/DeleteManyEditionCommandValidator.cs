using FluentValidation;

namespace Cemiyet.Application.Books.Commands.DeleteManyEdition
{
    public class DeleteManyEditionCommandValidator : AbstractValidator<DeleteManyEditionCommand>
    {
        public DeleteManyEditionCommandValidator()
        {
            RuleFor(dmec => dmec.Isbns).NotNull();
            RuleFor(dmec => dmec.Isbns.Length).GreaterThan(1);
            RuleForEach(dmec => dmec.Isbns).NotEmpty().When(dmec => dmec.Isbns.Length > 1);
        }
    }
}
