using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.Add
{
    public class AddHandler : IRequestHandler<AddCommand>
    {
        private readonly MainDataContext _context;

        public AddHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var dimension = new Dimension
            {
                Width = request.Width,
                Height = request.Height,
                CreationDate = DateTime.UtcNow,
                // CreatorId =
                // TODO (v0.4): add creator id.
            };

            _context.Dimensions.Add(dimension);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}