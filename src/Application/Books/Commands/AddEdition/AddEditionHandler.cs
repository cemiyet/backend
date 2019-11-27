using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.AddEdition
{
    public class AddEditionHandler : IRequestHandler<AddEditionCommand>
    {
        private readonly AppDataContext _context;

        public AddEditionHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddEditionCommand request, CancellationToken cancellationToken)
        {
            var bookEdition = new BookEdition
            {
                Isbn = request.Isbn,
                PageCount = request.PageCount,
                PrintDate = request.PrintDate,
                BooksId = request.BooksId,
                DimensionsId = request.DimensionsId,
                PublishersId = request.PublishersId,
                CreationDate = DateTime.UtcNow
                // CreatorId =
                // TODO (v0.5): add creator id.
            };

            _context.BookEditions.Add(bookEdition);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
