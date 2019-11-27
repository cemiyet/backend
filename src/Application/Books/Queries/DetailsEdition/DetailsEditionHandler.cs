using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Books.Queries.DetailsEdition
{
    public class DetailsEditionHandler : IRequestHandler<DetailsEditionQuery, BookEditionViewModel>
    {
        private readonly AppDataContext _context;

        public DetailsEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<BookEditionViewModel> Handle(DetailsEditionQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new BookNotFoundException(request.Id);

            var edition = book.Editions.Single(be => be.Isbn == request.Isbn);

            if (edition == null)
                throw new BookEditionNotFoundException(request.Isbn);

            return BookEditionViewModel.CreateFromBookEdition(edition, true, true, true);
        }
    }
}
