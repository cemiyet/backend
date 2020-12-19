using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Commands.Dimensions
{
    public class DeleteManyCommandHandler : IRequestHandler<DeleteManyCommand>
    {
        private readonly AppDataContext _context;

        public DeleteManyCommandHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteManyCommand request, CancellationToken cancellationToken)
        {
            var dimensions = _context.Dimensions.Where(d => request.Ids.Contains(d.Id));

            if (!dimensions.Any())
                throw new DimensionNotFoundException(request.Ids);

            _context.RemoveRange(dimensions);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
