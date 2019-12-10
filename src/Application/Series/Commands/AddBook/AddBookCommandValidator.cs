using System;
using System.Collections.Generic;
using FluentValidation;

namespace Cemiyet.Application.Series.Commands.AddBook
{
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
