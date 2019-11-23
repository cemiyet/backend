using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Publishers.Queries.ListBooks
{
    public class ListBooksHandler : IRequestHandler<ListBooksQuery, List<BookEditionViewModel>>
    {
        private readonly AppDataContext _context;

        public ListBooksHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<BookEditionViewModel>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
        {
            var publisher = await _context.Publishers.FindAsync(request.Id);

            if (publisher == null)
                throw new PublisherNotFoundException(request.Id);

            return BookEditionViewModel.CreateFromBookEditions(publisher.BookEditions, true, true, true).ToList();
        }
    }
}
