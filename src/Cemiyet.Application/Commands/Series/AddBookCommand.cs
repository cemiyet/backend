using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Series
{
    public class AddBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, short> Books { get; set; }
    }

    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(abc => abc.Id).NotNull();
            RuleFor(abc => abc.Books).NotEmpty();
            RuleForEach(abc => abc.Books).Must(HaveValidData);
        }

        private bool HaveValidData(KeyValuePair<Guid, short> data)
        {
            return data.Key != Guid.Empty && data.Value >= 1;
        }
    }
}
