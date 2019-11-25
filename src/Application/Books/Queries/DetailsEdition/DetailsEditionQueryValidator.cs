using FluentValidation;

namespace Cemiyet.Application.Books.Queries.DetailsEdition
{
    public class DetailsEditionQueryValidator : AbstractValidator<DetailsEditionQuery>
    {
        public DetailsEditionQueryValidator()
        {
            RuleFor(leq => leq.Id).NotEmpty();
            RuleFor(leq => leq.Isbn).NotEmpty();
        }
    }
}
