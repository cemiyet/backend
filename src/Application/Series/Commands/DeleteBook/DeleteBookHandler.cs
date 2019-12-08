using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Series.Commands.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly AppDataContext _context;

        public DeleteBookHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var serie = await _context.Series.FindAsync(request.Id);
            if (serie == null) throw new SerieNotFoundException(request.Id);

            var serieBooks = serie.Books.Where(sb => request.BookIds.Contains(sb.Book.Id));
            if (!serieBooks.Any()) throw new BookNotFoundException(request.BookIds);

            foreach (var book in serieBooks.ToList())
                serie.Books.Remove(book);

            var success = await _context.SaveChangesAsync() > 0;
            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
