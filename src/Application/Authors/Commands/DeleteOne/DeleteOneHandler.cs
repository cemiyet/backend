using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Authors.Commands.DeleteOne
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
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new AuthorNotFoundException(request.Id);

            _context.Remove(author);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
