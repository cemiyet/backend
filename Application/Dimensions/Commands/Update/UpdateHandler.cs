using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.Update
{
    public class UpdateHandler : IRequestHandler<UpdateCommand>
    {
        private readonly MainDataContext _context;

        public UpdateHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCommand request,
            CancellationToken cancellationToken)
        {
            var dimension = await _context.Dimensions.FindAsync(request.Id);

            // TODO (v0.1): create and use more friendly exception.
            if (dimension == null)
                throw new Exception("Could not found dimension with specified id.");

            dimension.Width = request.Width;
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
