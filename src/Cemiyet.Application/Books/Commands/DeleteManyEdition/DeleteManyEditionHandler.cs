using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.DeleteManyEdition
{
    public class DeleteManyEditionHandler : IRequestHandler<DeleteManyEditionCommand>
    {
        private readonly AppDataContext _context;

        public DeleteManyEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteManyEditionCommand request, CancellationToken cancellationToken)
        {
            var bookEditions = _context.BookEditions.Where(b => request.Isbns.Contains(b.Isbn));

            if (!bookEditions.Any())
                throw new BookEditionNotFoundException(request.Isbns);

            _context.RemoveRange(bookEditions);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
