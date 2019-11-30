using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.UpdatePartially
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
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
                throw new BookNotFoundException(request.Id);

            if (!string.IsNullOrEmpty(request.Title) && request.Title != book.Title)
                book.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Description) && request.Description != book.Description)
                book.Description = request.Description;

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
