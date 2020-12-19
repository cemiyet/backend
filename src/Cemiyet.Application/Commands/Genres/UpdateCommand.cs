using System;
using System.Linq;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cemiyet.Application.Commands.Genres
{
    public class UpdateCommand : IRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(uc => uc.Id).NotNull();

            RuleFor(uc => uc.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(ShouldNotContainDigits)
                .WithMessage("'Name' should not contain any digits.")
                .MaximumLength(50);
        }

        private bool ShouldNotContainDigits(string s)
        {
            return !s.Any(char.IsDigit);
        }
    }
}
