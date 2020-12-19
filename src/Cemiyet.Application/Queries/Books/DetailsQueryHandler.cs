using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Queries.Books
{
    public class DetailsQueryHandler : IRequestHandler<DetailsQuery, BookViewModel>
    {
        private readonly AppDataContext _context;

        public DetailsQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<BookViewModel> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new BookNotFoundException(request.Id);

            return BookViewModel.CreateFromBook(book, true, true, true);
        }
    }
}
