using FluentValidation;

namespace Cemiyet.Application.Series.Commands.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(abc => abc.Id).NotNull();
            RuleForEach(abc => abc.Books).NotEmpty();
            // should validate order > 0
        }
    }
}
