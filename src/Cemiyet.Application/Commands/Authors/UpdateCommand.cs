using System;
using System.Linq;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Authors
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
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
                .WithMessage("Name alanı sayısal karakter içermemeli.")
                .MaximumLength(25);

            RuleFor(uc => uc.Surname)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(ShouldNotContainDigits)
                .WithMessage("Surname alanı sayısal karakter içermemeli.")
                .MaximumLength(25);

            RuleFor(uc => uc.Bio)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(2000);
        }

        private bool ShouldNotContainDigits(string s)
        {
            return !s.Any(char.IsDigit);
        }
    }
}
