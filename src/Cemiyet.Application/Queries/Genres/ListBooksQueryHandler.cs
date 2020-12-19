using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Genres
{
    public class ListBooksQueryHandler : IRequestHandler<ListBooksQuery, List<BookViewModel>>
    {
        private readonly AppDataContext _context;

        public ListBooksQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<BookViewModel>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.FindAsync(request.Id);

            if (genre == null)
                throw new GenreNotFoundException(request.Id);

            return BookViewModel.CreateFromBooks(genre.Books.Select(gb => gb.Book).PagedToList(request.Page, request.PageSize),
                                                 true, true, true, true).ToList();
        }
    }
}
