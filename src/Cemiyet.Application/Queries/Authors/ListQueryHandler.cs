using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Authors
{
    public class ListQueryHandler : IRequestHandler<ListQuery, List<AuthorViewModel>>
    {
        private readonly AppDataContext _context;

        public ListQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorViewModel>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var authorSet = await _context.Authors.PagedToListAsync(request.Page, request.PageSize);
            return AuthorViewModel.CreateFromAuthors(authorSet).ToList();
        }
    }
}
