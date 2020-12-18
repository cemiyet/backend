using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Application.Books.Commands.UpdatePartiallyEdition
{
    public class UpdatePartiallyEditionHandler : IRequestHandler<UpdatePartiallyEditionCommand>
    {
        private readonly AppDataContext _context;

        public UpdatePartiallyEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePartiallyEditionCommand request, CancellationToken cancellationToken)
        {
            var bookEdition = await _context.BookEditions.FindAsync(request.Isbn);
            if (bookEdition == null) throw new BookEditionNotFoundException(request.Isbn);

            var book = await _context.Books.FindAsync(request.Id);
            if (book == null) throw new BookNotFoundException(request.Id);

            if (!string.IsNullOrEmpty(request.NewIsbn) && request.NewIsbn != request.Isbn)
                bookEdition.Isbn = request.NewIsbn;

            if (request.PageCount != default && request.PageCount != bookEdition.PageCount)
                bookEdition.PageCount = request.PageCount;

            if (request.PrintDate != default && request.PrintDate != bookEdition.PrintDate)
                bookEdition.PrintDate = request.PrintDate;

            if (request.BooksId != default && request.BooksId != bookEdition.BooksId)
            {
                book = await _context.Books.FindAsync(request.BooksId);
                if (book == null) throw new BookNotFoundException(request.BooksId);
                bookEdition.Book = book;
            }

            if (request.DimensionsId != default && request.DimensionsId != bookEdition.DimensionsId)
            {
                var dimension = await _context.Dimensions.FindAsync(request.DimensionsId);
                bookEdition.Dimensions = dimension ?? throw new DimensionNotFoundException(request.DimensionsId);
            }

            if (request.PublishersId != default && request.PublishersId != bookEdition.PublishersId)
            {
                var publisher = await _context.Publishers.FindAsync(request.PublishersId);
                bookEdition.Publisher = publisher ?? throw new PublisherNotFoundException(request.PublishersId);
            }

            if (_context.Entry(bookEdition).State != EntityState.Modified)
                throw new Exception("Nothing updated.");

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
