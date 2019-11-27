using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Books.Commands.Add
{
    public class AddHandler : IRequestHandler<AddCommand>
    {
        private readonly AppDataContext _context;

        public AddHandler(AppDataContext context)
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
            };

            foreach (var genreId in request.Genres)
            {
                var genre = _context.Genres.Find(genreId);

                if (genre == null)
                    throw new GenreNotFoundException(genreId);

                book.Genres.Add(new BooksGenres { Genre = genre });
            }

            foreach (var authorId in request.Authors)
            {
                var author = _context.Authors.Find(authorId);

                if (author == null)
                    throw new AuthorNotFoundException(authorId);

                book.Authors.Add(new AuthorsBooks { Author = author });
            }

            _context.Books.Add(book);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}