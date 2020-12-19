using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Books.Queries.List
{
    public class ListHandler : IRequestHandler<ListQuery, List<BookViewModel>>
    {
        private readonly AppDataContext _context;

        public ListHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<BookViewModel>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var bookSet = await _context.Books.PagedToListAsync(request.Page, request.PageSize);
            return BookViewModel.CreateFromBooks(bookSet, true, true, true, true).ToList();
        }
    }
}
