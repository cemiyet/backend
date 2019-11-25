using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Books.Queries.ListEdition
{
    public class ListEditionHandler : IRequestHandler<ListEditionQuery, List<BookEditionViewModel>>
    {
        private readonly AppDataContext _context;

        public ListEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<BookEditionViewModel>> Handle(ListEditionQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new BookNotFoundException(request.Id);

            return BookEditionViewModel.CreateFromBookEditions(book.Editions, true, true, true).ToList();
        }
    }
}
