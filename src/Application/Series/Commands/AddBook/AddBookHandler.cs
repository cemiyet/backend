using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Series.Commands.AddBook
{
    public class AddBookHandler : IRequestHandler<AddBookCommand>
    {
        private readonly AppDataContext _context;

        public AddBookHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var serie = await _context.Series.FindAsync(request.Id);

            if (serie == null) throw new SerieNotFoundException(request.Id);

            var existingBooksIds = serie.Books.Select(sb => sb.Book.Id);
            var newBooks = request.Books.Where(bd => !existingBooksIds.Contains(bd.Key)).ToList();
            var books = _context.Books.Where(b => newBooks.Select(bd => bd.Key).Contains(b.Id)).ToList();

            if (!books.Any()) throw new BookNotFoundException(request.Books.Keys);

            foreach (var book in books)
                serie.Books.Add(new SeriesBooks
                {
                    Order = newBooks.First(bd => bd.Key == book.Id).Value,
                    Book = book
                });

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
