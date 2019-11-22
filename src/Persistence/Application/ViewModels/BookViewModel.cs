using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class BookViewModel : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }
        public ICollection<AuthorViewModel> Authors { get; set; }
        public ICollection<BookEditionViewModel> Editions { get; set; }

        public static BookViewModel CreateFromBook(Book book,
                                                   bool includeGenres = false,
                                                   bool includeAuthors = false,
                                                   bool includeEditions = false)
        {
            var dto = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                CreationDate = book.CreationDate,
                CreatorId = book.CreatorId
            };

            if (includeGenres)
                dto.Genres = GenreViewModel.CreateFromGenres(book.Genres.Select(bg => bg.Genre).ToList());

            if (includeAuthors)
                dto.Authors = AuthorViewModel.CreateFromAuthors(book.Authors.Select(ba => ba.Author).ToList());

            if (includeEditions)
                dto.Editions = BookEditionViewModel.CreateFromBookEditions(book.Editions);

            return dto;
        }

        public static ICollection<BookViewModel> CreateFromBooks(ICollection<Book> books,
                                                                 bool includeGenres = false,
                                                                 bool includeAuthors = false,
                                                                 bool includeEditions = false)
        {
            return books.Select(b => CreateFromBook(b, includeGenres, includeAuthors, includeEditions)).ToList();
        }
    }
}
