using FluentValidation;

namespace Cemiyet.Application.Authors.Queries.Details
{
    public class DetailsQueryValidator : AbstractValidator<DetailsQuery>
    {
        public DetailsQueryValidator()
        {
            RuleFor(dq => dq.Id).NotEmpty();
        }
    }
}
