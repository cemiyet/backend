using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Books
{
    public class ListEditionsQueryHandler : IRequestHandler<ListEditionsQuery, List<BookEditionViewModel>>
    {
        private readonly AppDataContext _context;

        public ListEditionsQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<BookEditionViewModel>> Handle(ListEditionsQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new BookNotFoundException(request.Id);

            return BookEditionViewModel.CreateFromBookEditions(book.Editions.PagedToList(request.Page, request.PageSize),
                                                               true, true, true).ToList();
        }
    }
}
