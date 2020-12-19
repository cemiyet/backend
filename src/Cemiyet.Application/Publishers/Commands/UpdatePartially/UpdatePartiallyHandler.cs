using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Application.Publishers.Commands.UpdatePartially
{
    public class UpdatePartiallyHandler : IRequestHandler<UpdatePartiallyCommand>
    {
        private readonly AppDataContext _context;

        public UpdatePartiallyHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePartiallyCommand request, CancellationToken cancellationToken)
        {
            var publisher = await _context.Publishers.FindAsync(request.Id);

            if (publisher == null)
                throw new PublisherNotFoundException(request.Id);

            if (!string.IsNullOrEmpty(request.Name) && request.Name != publisher.Name)
                publisher.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Description) && request.Description != publisher.Description)
                publisher.Description = request.Description;

            if (_context.Entry(publisher).State != EntityState.Modified)
                throw new Exception("Nothing updated.");

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
