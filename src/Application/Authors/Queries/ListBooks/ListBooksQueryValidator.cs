using FluentValidation;

namespace Cemiyet.Application.Authors.Queries.ListBooks
{
    public class ListBooksQueryValidator : AbstractValidator<ListBooksQuery>
    {
        public ListBooksQueryValidator()
        {
            RuleFor(lb => lb.Id).NotNull();
            RuleFor(pm => pm.Page).GreaterThan(0);
            RuleFor(pm => pm.PageSize).GreaterThan(0);
        }
    }
}
