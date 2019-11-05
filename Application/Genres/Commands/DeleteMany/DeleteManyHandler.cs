using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
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

            if (!genres.Any())
                throw new GenreNotFoundException(request.Ids);

            _context.RemoveRange(genres);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
