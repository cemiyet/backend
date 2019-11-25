using FluentValidation;

namespace Cemiyet.Application.Books.Queries.ListEdition
{
    public class ListEditionQueryValidator : AbstractValidator<ListEditionQuery>
    {
        public ListEditionQueryValidator()
        {
            RuleFor(lq => lq.Id).NotNull();
            RuleFor(pm => pm.Page).GreaterThan(0);
            RuleFor(pm => pm.PageSize).GreaterThan(0);
        }
    }
}
