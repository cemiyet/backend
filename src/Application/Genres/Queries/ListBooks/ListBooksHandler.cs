using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.ListBooks
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
            var genre = await _context.Genres.FindAsync(request.Id);

            if (genre == null)
                throw new GenreNotFoundException(request.Id);

            return BookViewModel.CreateFromBooks(genre.Books.Select(gb => gb.Book).ToList(), true, true, true).ToList();
        }
    }
}
