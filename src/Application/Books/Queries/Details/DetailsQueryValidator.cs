using FluentValidation;

namespace Cemiyet.Application.Books.Queries.Details
{
    public class DetailsQueryValidator : AbstractValidator<DetailsQuery>
    {
        public DetailsQueryValidator()
        {
            RuleFor(dq => dq.Id).NotEmpty();
        }
    }
}
