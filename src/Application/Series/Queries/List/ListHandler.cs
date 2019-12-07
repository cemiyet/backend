using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Series.Queries.List
{
    public class ListHandler : IRequestHandler<ListQuery, List<SerieViewModel>>
    {
        private readonly AppDataContext _context;

        public ListHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<SerieViewModel>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var serieSet = await _context.Series.PagedToListAsync(request.Page, request.PageSize);
            return SerieViewModel.CreateFromSeries(serieSet, true, true).ToList();
        }
    }
}
