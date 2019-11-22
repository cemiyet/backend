using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.Add
{
    public class AddHandler : IRequestHandler<AddCommand>
    {
        private readonly AppDataContext _context;

        public AddHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Name = request.Name,
                CreationDate = DateTime.UtcNow,
                // CreatorId =
                // TODO (v0.5): add creator id.
            };

            _context.Genres.Add(genre);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
