using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.Update
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
            var dimension = await _context.Dimensions.FindAsync(request.Id);

            if (dimension == null)
                throw new DimensionNotFoundException(request.Id);

            dimension.Width = request.Width;
            dimension.Height = request.Height;
            // dimension.ModifierId =
            // TODO (v0.4): add modifier id.

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
