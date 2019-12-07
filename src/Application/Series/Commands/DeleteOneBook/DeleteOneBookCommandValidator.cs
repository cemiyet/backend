using FluentValidation;

namespace Cemiyet.Application.Series.Commands.DeleteOneBook
{
    public class DeleteOneBookCommandValidator : AbstractValidator<DeleteOneBookCommand>
    {
        public DeleteOneBookCommandValidator()
        {
            RuleFor(dobc => dobc.Id).NotEmpty();
            RuleFor(dobc => dobc.BookId).NotEmpty();
        }
    }
}
