using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Application.Commands.Books
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
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new BookNotFoundException(request.Id);

            if (!string.IsNullOrEmpty(request.Title) && request.Title != book.Title)
                book.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Description) && request.Description != book.Description)
                book.Description = request.Description;

            if (_context.Entry(book).State != EntityState.Modified)
                throw new Exception("Nothing updated.");

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
