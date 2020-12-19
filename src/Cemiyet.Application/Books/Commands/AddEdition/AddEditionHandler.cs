using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.AddEdition
{
    public class AddEditionHandler : IRequestHandler<AddEditionCommand>
    {
        private readonly AppDataContext _context;

        public AddEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddEditionCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.BooksId);

            if (book == null)
                throw new BookNotFoundException(request.BooksId);

            var dimension = await _context.Dimensions.FindAsync(request.DimensionsId);

            if (dimension == null)
                throw new DimensionNotFoundException(request.DimensionsId);

            var publisher = await _context.Publishers.FindAsync(request.PublishersId);

            if (publisher == null)
                throw new PublisherNotFoundException(request.PublishersId);

            var bookEdition = new BookEdition
            {
                Isbn = request.Isbn,
                PageCount = request.PageCount,
                PrintDate = request.PrintDate,
                Book = book,
                Dimensions = dimension,
                Publisher = publisher,
                CreationDate = DateTime.UtcNow
                // CreatorId =
                // TODO (v0.5): add creator id.
            };

            _context.BookEditions.Add(bookEdition);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
