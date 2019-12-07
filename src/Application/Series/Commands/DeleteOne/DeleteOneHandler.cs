using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Series.Commands.DeleteOne
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
            var serie = await _context.Series.FindAsync(request.Id);

            if (serie == null)
                throw new SerieNotFoundException(request.Id);

            _context.Remove(serie);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
