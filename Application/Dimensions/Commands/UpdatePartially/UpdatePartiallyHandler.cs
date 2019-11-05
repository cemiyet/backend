using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.UpdatePartially
{
    public class UpdatePartiallyHandler : IRequestHandler<UpdatePartiallyCommand>
    {
        private readonly MainDataContext _context;

        public UpdatePartiallyHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePartiallyCommand request, CancellationToken cancellationToken)
        {
            var dimension = await _context.Dimensions.FindAsync(request.Id);

            if (dimension == null)
                throw new DimensionNotFoundException(request.Id);

            if (!request.Width.Equals(default) && Math.Abs(request.Width - dimension.Width) > 0.1)
                dimension.Width = request.Width;

            if (!request.Height.Equals(default) && Math.Abs(request.Width - dimension.Width) > 0.1)
                dimension.Height = request.Height;

            dimension.ModificationDate = DateTime.UtcNow;
            // dimension.ModifierId =
            // TODO (v0.4): add modifier id.

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
