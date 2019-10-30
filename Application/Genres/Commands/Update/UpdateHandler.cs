using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.Update
{
    public class UpdateHandler : IRequestHandler<UpdateCommand>
    {
        private readonly MainDataContext _context;

        public UpdateHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.FindAsync(request.Id);

            // TODO (v0.1): create and use more friendly exception.
            if (genre == null)
                throw new Exception("Could not found genre with specified id.");

            genre.Name = request.Name ?? genre.Name;
            genre.ModificationDate = DateTime.UtcNow;
            // genre.ModifierId =
            // TODO (v0.4): add modifier id.


            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
