using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Authors.Queries.List
{
    public class ListHandler : IRequestHandler<ListQuery, List<Author>>
    {
        private readonly AppDataContext _context;

        public ListHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Authors.PagedToListAsync(request.Page, request.PageSize);
        }
    }
}
