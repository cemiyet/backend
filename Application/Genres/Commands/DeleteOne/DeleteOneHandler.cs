using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.DeleteOne
{
    public class DeleteOneHandler : IRequestHandler<DeleteOneCommand>
    {
        private readonly MainDataContext _context;

        public DeleteOneHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOneCommand request, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.FindAsync(request.Id);

            // TODO (v0.1): create and use more friendly exception.
            if (genre == null)
                throw new Exception("Could not found genre with specified id.");

            _context.Remove(genre);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
