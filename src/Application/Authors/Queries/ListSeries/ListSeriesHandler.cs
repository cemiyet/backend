using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Authors.Queries.ListSeries
{
    public class ListSeriesHandler : IRequestHandler<ListSeriesQuery, List<SerieViewModel>>
    {
        private readonly AppDataContext _context;

        public ListSeriesHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<SerieViewModel>> Handle(ListSeriesQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new AuthorNotFoundException(request.Id);

            return SerieViewModel.CreateFromSeries(author.Series.Select(sa => sa.Serie).PagedToList(request.Page, request.PageSize),
                                                   true, true).ToList();
        }
    }
}
