using FluentValidation;

namespace Cemiyet.Application.Books.Commands.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(ac => ac.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(ac => ac.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(2500);

            RuleFor(ac => ac.GenreIds).NotEmpty();
            RuleFor(ac => ac.AuthorIds).NotEmpty();
        }
    }
}
