using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Dimensions
{
    public class ListQueryHandler : IRequestHandler<ListQuery, List<DimensionViewModel>>
    {
        private readonly AppDataContext _context;

        public ListQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<DimensionViewModel>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var dimensionSet = await _context.Dimensions.PagedToListAsync(request.Page, request.PageSize);
            return DimensionViewModel.CreateFromDimensions(dimensionSet).ToList();
        }
    }
}
