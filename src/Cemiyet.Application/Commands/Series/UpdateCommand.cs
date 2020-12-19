using System;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Series
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(uc => uc.Id).NotNull();

            RuleFor(uc => uc.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(uc => uc.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
