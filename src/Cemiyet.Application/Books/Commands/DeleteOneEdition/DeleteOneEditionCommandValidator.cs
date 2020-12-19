using FluentValidation;

namespace Cemiyet.Application.Books.Commands.DeleteOneEdition
{
    public class DeleteOneEditionCommandValidator : AbstractValidator<DeleteOneEditionCommand>
    {
        public DeleteOneEditionCommandValidator()
        {
            RuleFor(doec => doec.Isbn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(13);
        }
    }
}
