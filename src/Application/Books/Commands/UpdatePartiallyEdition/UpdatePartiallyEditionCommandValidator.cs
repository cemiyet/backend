using System;
using FluentValidation;

namespace Cemiyet.Application.Books.Commands.UpdatePartiallyEdition
{
    public class UpdatePartiallyEditionCommandValidator : AbstractValidator<UpdatePartiallyEditionCommand>
    {
        public UpdatePartiallyEditionCommandValidator()
        {
            RuleFor(upec => upec.Id).NotNull();
            RuleFor(upec => upec.Isbn).Length(13).When(upec => !string.IsNullOrEmpty(upec.Isbn));
            RuleFor(upec => upec.NewIsbn).Length(13).When(upec => !string.IsNullOrEmpty(upec.NewIsbn));

            RuleFor(upec => upec.PageCount).NotEmpty().When(upec =>
                                                                upec.PrintDate == default &&
                                                                upec.BooksId == default &&
                                                                upec.DimensionsId == default &&
                                                                upec.PublishersId == default);

            RuleFor(upec => upec.PrintDate).NotEmpty().When(upec =>
                                                                upec.PageCount == default &&
                                                                upec.BooksId == default &&
                                                                upec.DimensionsId == default &&
                                                                upec.PublishersId == default);

            RuleFor(upec => upec.BooksId).NotEmpty().When(upec =>
                                                              upec.PageCount == default &&
                                                              upec.PrintDate == default &&
                                                              upec.DimensionsId == default &&
                                                              upec.PublishersId == default);

            RuleFor(upec => upec.DimensionsId).NotEmpty().When(upec =>
                                                                   upec.PageCount == default &&
                                                                   upec.PrintDate == default &&
                                                                   upec.BooksId == default &&
                                                                   upec.PublishersId == default);

            RuleFor(upec => upec.PublishersId).NotEmpty().When(upec =>
                                                                   upec.PageCount == default &&
                                                                   upec.PrintDate == default &&
                                                                   upec.BooksId == default &&
                                                                   upec.DimensionsId == default);
        }
    }
}
