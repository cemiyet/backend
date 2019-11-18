using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Authors.Commands.DeleteMany
{
    public class DeleteManyHandler : IRequestHandler<DeleteManyCommand>
    {
        private readonly AppDataContext _context;

        public DeleteManyHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteManyCommand request, CancellationToken cancellationToken)
        {
            var authors = _context.Authors.Where(a => request.Ids.Contains(a.Id));

            if (!authors.Any())
                throw new AuthorNotFoundException(request.Ids);

            _context.RemoveRange(authors);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
