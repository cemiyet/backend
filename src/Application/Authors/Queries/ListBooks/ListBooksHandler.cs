using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Authors.Queries.ListBooks
{
    public class ListBooksHandler : IRequestHandler<ListBooksQuery, List<BookViewModel>>
    {
        private readonly AppDataContext _context;

        public ListBooksHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<BookViewModel>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new AuthorNotFoundException(request.Id);

            return BookViewModel.CreateFromBooks(author.Books.Select(gb => gb.Book).PagedToList(request.Page, request.PageSize),
                                                 true, true, true).ToList();
        }
    }
}
