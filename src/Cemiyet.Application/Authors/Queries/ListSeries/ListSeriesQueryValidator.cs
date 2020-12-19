using FluentValidation;

namespace Cemiyet.Application.Authors.Queries.ListSeries
{
    public class ListSeriesQueryValidator : AbstractValidator<ListSeriesQuery>
    {
        public ListSeriesQueryValidator()
        {
            RuleFor(ls => ls.Id).NotEmpty();
            RuleFor(pm => pm.Page).GreaterThan(0);
            RuleFor(pm => pm.PageSize).GreaterThan(0);
        }
    }
}
