using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.DeleteOneEdition
{
    public class DeleteOneEditionHandler : IRequestHandler<DeleteOneEditionCommand>
    {
        private readonly AppDataContext _context;

        public DeleteOneEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOneEditionCommand request, CancellationToken cancellationToken)
        {
            var bookEdition = await _context.BookEditions.FindAsync(request.Isbn);

            if (bookEdition == null)
                throw new BookEditionNotFoundException(request.Isbn);

            _context.Remove(bookEdition);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
