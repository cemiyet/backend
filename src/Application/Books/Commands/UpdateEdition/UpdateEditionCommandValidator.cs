using Cemiyet.Core;
using FluentValidation;

namespace Cemiyet.Application.Books.Commands.UpdateEdition
{
    public class UpdateEditionCommandValidator : AbstractValidator<UpdateEditionCommand>
    {
        public UpdateEditionCommandValidator()
        {
            RuleFor(uec => uec.Isbn)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Length(13);

            RuleFor(uec => uec.PageCount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThanOrEqualTo(Constants.BookEditionMinPageSize);

            RuleFor(uec => uec.Id).NotNull();
            RuleFor(uec => uec.Isbn).NotEmpty();
            RuleFor(uec => uec.PrintDate).NotEmpty();
            RuleFor(uec => uec.BooksId).NotEmpty();
            RuleFor(uec => uec.DimensionsId).NotEmpty();
            RuleFor(uec => uec.PublishersId).NotEmpty();
        }
    }
}
