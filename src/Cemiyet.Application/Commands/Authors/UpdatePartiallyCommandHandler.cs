using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Application.Commands.Authors
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
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new AuthorNotFoundException(request.Id);

            if (!string.IsNullOrEmpty(request.Name) && request.Name != author.Name)
                author.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Surname) && request.Surname != author.Surname)
                author.Surname = request.Surname;

            if (!string.IsNullOrEmpty(request.Bio) && request.Bio != author.Bio)
                author.Bio = request.Bio;

            if (_context.Entry(author).State != EntityState.Modified)
                throw new Exception("Nothing updated.");

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
