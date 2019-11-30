using Cemiyet.Core;
using FluentValidation;

namespace Cemiyet.Application.Books.Commands.UpdateEdition
{
    public class UpdateEditionCommandValidator : AbstractValidator<UpdateEditionCommand>
    {
        public UpdateEditionCommandValidator()
        {
            RuleFor(uec => uec.Isbn).Length(13).When(uec => !string.IsNullOrEmpty(uec.Isbn));
            RuleFor(uec => uec.NewIsbn).Length(13).When(uec => !string.IsNullOrEmpty(uec.NewIsbn));

            RuleFor(uec => uec.PageCount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThanOrEqualTo(Constants.BookEditionMinPageSize);

            RuleFor(uec => uec.Id).NotNull();
            RuleFor(uec => uec.PrintDate).NotEmpty();
            RuleFor(uec => uec.BooksId).NotEmpty();
            RuleFor(uec => uec.DimensionsId).NotEmpty();
            RuleFor(uec => uec.PublishersId).NotEmpty();
        }
    }
}
