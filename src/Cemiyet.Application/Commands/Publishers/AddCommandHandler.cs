using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Commands.Publishers
{
    public class AddCommandHandler : IRequestHandler<AddCommand>
    {
        private readonly AppDataContext _context;

        public AddCommandHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var publisher = new Publisher
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Publishers.Add(publisher);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
