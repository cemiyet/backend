using FluentValidation;

namespace Cemiyet.Application.Books.Queries.DetailsEdition
{
    public class DetailsEditionQueryValidator : AbstractValidator<DetailsEditionQuery>
    {
        public DetailsEditionQueryValidator()
        {
            RuleFor(deq => deq.Id).NotEmpty();
            RuleFor(deq => deq.Isbn)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(13);
        }
    }
}
