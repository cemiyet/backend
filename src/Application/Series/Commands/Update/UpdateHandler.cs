using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Series.Commands.Update
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
            var serie = await _context.Series.FindAsync(request.Id);
            if (serie == null) throw new SerieNotFoundException(request.Id);

            serie.Title = request.Title;
            serie.Description = request.Description;

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
