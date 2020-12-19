using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Application.Commands.Dimensions
{
    public class UpdatePartiallyCommandHandler : IRequestHandler<UpdatePartiallyCommand>
    {
        private readonly AppDataContext _context;

        public UpdatePartiallyCommandHandler(AppDataContext context)
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

            if (_context.Entry(dimension).State != EntityState.Modified)
                throw new Exception("Nothing updated.");

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
