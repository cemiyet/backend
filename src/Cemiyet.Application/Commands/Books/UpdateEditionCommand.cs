using System;
using Cemiyet.Core;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Books
{
    public class UpdateEditionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
        public string NewIsbn { get; set; }
        public short PageCount { get; set; }
        public DateTime PrintDate { get; set; }

        public Guid BooksId { get; set; }
        public Guid DimensionsId { get; set; }
        public Guid PublishersId { get; set; }
    }

    public class UpdateEditionCommandValidator : AbstractValidator<UpdateEditionCommand>
    {
        public UpdateEditionCommandValidator()
        {
            RuleFor(uec => uec.Isbn).Length(13).When(uec => !string.IsNullOrEmpty(uec.Isbn));
            RuleFor(uec => uec.NewIsbn).Length(13).When(uec => !string.IsNullOrEmpty(uec.NewIsbn));

            RuleFor(uec => uec.PageCount)
                .Cascade(CascadeMode.Stop)
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
