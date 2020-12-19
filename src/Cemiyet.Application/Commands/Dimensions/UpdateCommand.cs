using System;
using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Dimensions
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(uc => uc.Id).NotNull();

            RuleFor(uc => uc.Width)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);

            RuleFor(uc => uc.Height)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);
        }
    }
}
