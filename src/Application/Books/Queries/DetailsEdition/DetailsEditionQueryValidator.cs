using FluentValidation;

namespace Cemiyet.Application.Books.Queries.DetailsEdition
{
    public class DetailsEditionQueryValidator : AbstractValidator<DetailsEditionQuery>
    {
        public DetailsEditionQueryValidator()
        {
            RuleFor(deq => deq.Id).NotEmpty();
            RuleFor(deq => deq.Isbn).NotEmpty();
        }
    }
}
