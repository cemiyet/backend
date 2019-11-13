using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.List
{
    public class ListHandler : IRequestHandler<ListQuery, List<Genre>>
    {
        private readonly AppDataContext _context;

        public ListHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Genres.PagedToListAsync(request.Page, request.PageSize);
        }
    }
}
