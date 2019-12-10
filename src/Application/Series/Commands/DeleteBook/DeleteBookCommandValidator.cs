using FluentValidation;

namespace Cemiyet.Application.Series.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(dbc => dbc.Id).NotNull();
            RuleFor(dbc => dbc.BookIds).NotEmpty();
            RuleForEach(dbc => dbc.BookIds).NotEmpty();
        }
    }
}
