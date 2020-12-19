using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Commands.Books
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
            var book = new Book {
                Title = request.Title,
                Description = request.Description,
                Genres = new List<BooksGenres>(),
                Authors = new List<AuthorsBooks>(),
                CreationDate = DateTime.UtcNow
                // CreatorId =
                // TODO (v0.5): add creator id.
            };

            foreach (var genreId in request.GenreIds)
            {
                var genre = await _context.Genres.FindAsync(genreId);

                if (genre == null)
                    throw new GenreNotFoundException(genreId);

                book.Genres.Add(new BooksGenres { Genre = genre });
            }

            foreach (var authorId in request.AuthorIds)
            {
                var author = await _context.Authors.FindAsync(authorId);

                if (author == null)
                    throw new AuthorNotFoundException(authorId);

                book.Authors.Add(new AuthorsBooks { Author = author });
            }

            _context.Books.Add(book);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
