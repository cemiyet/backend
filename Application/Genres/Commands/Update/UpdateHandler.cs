using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.Update
{
    public class UpdateHandler : IRequestHandler<UpdateCommand>
    {
        private readonly AppDataContext _context;

        public UpdateHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.FindAsync(request.Id);

            if (genre == null)
                throw new GenreNotFoundException(request.Id);

            genre.Name = request.Name;
            // genre.ModifierId =
            // TODO (v0.4): add modifier id.

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
