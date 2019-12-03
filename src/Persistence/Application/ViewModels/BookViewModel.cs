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
        public ICollection<SerieViewModel> Series { get; set; }

        public static BookViewModel CreateFromBook(Book book,
                                                   bool includeGenres = false,
                                                   bool includeAuthors = false,
                                                   bool includeEditions = false,
                                                   bool includeSeries = false)
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
                dto.Editions = BookEditionViewModel.CreateFromBookEditions(book.Editions, false, true, true);

            if (includeSeries)
                dto.Series = SerieViewModel.CreateFromSeries(book.Series.Select(sb => sb.Serie).ToList(), true);

            return dto;
        }

        public static ICollection<BookViewModel> CreateFromBooks(ICollection<Book> books,
                                                                 bool includeGenres = false,
                                                                 bool includeAuthors = false,
                                                                 bool includeEditions = false,
                                                                 bool includeSeries = false)
        {
            return books.Select(b => CreateFromBook(b, includeGenres, includeAuthors, includeEditions, includeSeries)).ToList();
        }
    }
}
