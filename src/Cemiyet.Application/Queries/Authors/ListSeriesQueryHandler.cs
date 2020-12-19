using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Core.Extensions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Authors
{
    public class ListSeriesQueryHandler : IRequestHandler<ListSeriesQuery, List<SerieViewModel>>
    {
        private readonly AppDataContext _context;

        public ListSeriesQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<SerieViewModel>> Handle(ListSeriesQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new AuthorNotFoundException(request.Id);

            return SerieViewModel.CreateFromSeries(author.Books.GetSeries().PagedToList(request.Page, request.PageSize),
                                                   true, true).ToList();
        }
    }
}
