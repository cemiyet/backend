using System;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Series
{
    public class DeleteBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid[] BookIds { get; set; }
    }

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
