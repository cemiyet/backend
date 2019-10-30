using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.DeleteMany
{
    public class DeleteManyHandler : IRequestHandler<DeleteManyCommand>
    {
        private readonly MainDataContext _context;

        public DeleteManyHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteManyCommand request, CancellationToken cancellationToken)
        {
            var genres = _context.Genres.Where(g => request.Ids.Contains(g.Id));

            // TODO (v0.1): create and use more friendly exception.
            if (!genres.Any())
                throw new Exception("Could not found any genre with specified ids.");

            _context.RemoveRange(genres);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
