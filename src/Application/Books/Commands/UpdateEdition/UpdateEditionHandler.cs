using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.UpdateEdition
{
    public class UpdateEditionHandler : IRequestHandler<UpdateEditionCommand>
    {
        private readonly AppDataContext _context;

        public UpdateEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEditionCommand request, CancellationToken cancellationToken)
        {
            var bookEdition = await _context.BookEditions.FindAsync(request.Isbn);

            if (bookEdition == null)
                throw new BookEditionNotFoundException(request.Isbn);

            var book = await _context.Books.FindAsync(request.BooksId);

            if (book == null)
                throw new BookNotFoundException(request.BooksId);

            var dimension = await _context.Dimensions.FindAsync(request.DimensionsId);

            if (dimension == null)
                throw new DimensionNotFoundException(request.DimensionsId);

            var publisher = await _context.Publishers.FindAsync(request.PublishersId);

            if (publisher == null)
                throw new PublisherNotFoundException(request.PublishersId);

            bookEdition.Isbn = request.Isbn;
            bookEdition.PageCount = request.PageCount;
            bookEdition.PrintDate = request.PrintDate;
            bookEdition.Book = book;
            bookEdition.Dimensions = dimension;
            bookEdition.Publisher = publisher;

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
