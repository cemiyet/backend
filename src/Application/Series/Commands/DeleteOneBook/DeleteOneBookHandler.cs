using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Series.Commands.DeleteOneBook
{
    public class DeleteOneBookHandler : IRequestHandler<DeleteOneBookCommand>
    {
        private readonly AppDataContext _context;

        public DeleteOneBookHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOneBookCommand request, CancellationToken cancellationToken)
        {
            var serie = await _context.Series.FindAsync(request.Id);
            if (serie == null) throw new SerieNotFoundException(request.Id);

            var serieBook = serie.Books.FirstOrDefault(sb => sb.Book.Id == request.BookId);
            if (serieBook == null) throw new BookNotFoundException(request.BookId);

            serie.Books.Remove(serieBook);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
