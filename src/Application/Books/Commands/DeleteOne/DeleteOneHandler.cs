using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.DeleteOne
{
    public class DeleteOneHandler : IRequestHandler<DeleteOneCommand>
    {
        private readonly AppDataContext _context;

        public DeleteOneHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOneCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new BookNotFoundException(request.Id);

            _context.Remove(book);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
