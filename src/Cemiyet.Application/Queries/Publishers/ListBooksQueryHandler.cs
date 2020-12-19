using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Publishers
{
    public class ListBooksQueryHandler : IRequestHandler<ListBooksQuery, List<BookEditionViewModel>>
    {
        private readonly AppDataContext _context;

        public ListBooksQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<BookEditionViewModel>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
        {
            var publisher = await _context.Publishers.FindAsync(request.Id);

            if (publisher == null)
                throw new PublisherNotFoundException(request.Id);

            return BookEditionViewModel.CreateFromBookEditions(publisher.BookEditions.PagedToList(request.Page, request.PageSize),
                                                               true, true, true).ToList();
        }
    }
}
