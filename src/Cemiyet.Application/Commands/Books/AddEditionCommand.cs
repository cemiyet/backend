using System;
using Cemiyet.Core;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Books
{
    public class AddEditionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
        public short PageCount { get; set; }
        public DateTime PrintDate { get; set; }

        public Guid BooksId { get; set; }
        public Guid DimensionsId { get; set; }
        public Guid PublishersId { get; set; }
    }

    public class AddEditionCommandValidator : AbstractValidator<AddEditionCommand>
    {
        public AddEditionCommandValidator()
        {
            RuleFor(aec => aec.Isbn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(13);

            RuleFor(aec => aec.PageCount)
                .Cascade(CascadeMode.Stop)
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
