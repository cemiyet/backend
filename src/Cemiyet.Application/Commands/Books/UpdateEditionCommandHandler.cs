using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Commands.Books
{
    public class UpdateEditionCommandHandler : IRequestHandler<UpdateEditionCommand>
    {
        private readonly AppDataContext _context;

        public UpdateEditionCommandHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEditionCommand request, CancellationToken cancellationToken)
        {
            var bookEdition = await _context.BookEditions.FindAsync(request.Isbn);

            if (bookEdition == null)
                throw new BookEditionNotFoundException(request.Isbn);

            var book = await _context.Books.FindAsync(request.BooksId);
            var dimension = await _context.Dimensions.FindAsync(request.DimensionsId);
            var publisher = await _context.Publishers.FindAsync(request.PublishersId);
            if (!string.IsNullOrEmpty(request.NewIsbn) && !request.NewIsbn.Equals(request.Isbn))
                bookEdition.Isbn = request.NewIsbn;

            bookEdition.PageCount = request.PageCount;
            bookEdition.PrintDate = request.PrintDate;
            bookEdition.Book = book ?? throw new BookNotFoundException(request.BooksId);
            bookEdition.Dimensions = dimension ?? throw new DimensionNotFoundException(request.DimensionsId);
            bookEdition.Publisher = publisher ?? throw new PublisherNotFoundException(request.PublishersId);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
