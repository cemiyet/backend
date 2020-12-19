using FluentValidation;
using MediatR;

namespace Cemiyet.Application.Commands.Series
{
    public class AddCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(ac => ac.Description).MaximumLength(2000);
        }
    }
}
