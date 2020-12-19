using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Dimensions
{
    public class AddCommand : IRequest
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Width)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);

            RuleFor(ac => ac.Height)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .GreaterThan(1);
        }
    }
}
