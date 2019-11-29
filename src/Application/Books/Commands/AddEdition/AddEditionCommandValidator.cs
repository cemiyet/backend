using Cemiyet.Core;
using FluentValidation;

namespace Cemiyet.Application.Books.Commands.AddEdition
{
    public class AddEditionCommandValidator : AbstractValidator<AddEditionCommand>
    {
        public AddEditionCommandValidator()
        {
            RuleFor(aec => aec.Isbn)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(13);

            RuleFor(aec => aec.PageCount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThanOrEqualTo(Constants.BookEditionMinPageSize);

            RuleFor(aec => aec.Id).NotNull();
            RuleFor(aec => aec.PrintDate).NotEmpty();
            RuleFor(aec => aec.BooksId).NotEmpty();
            RuleFor(aec => aec.DimensionsId).NotEmpty();
            RuleFor(aec => aec.PublishersId).NotEmpty();
        }
    }
}
