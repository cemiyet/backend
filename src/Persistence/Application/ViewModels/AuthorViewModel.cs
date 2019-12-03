using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class AuthorViewModel : Entity<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }

        public ICollection<BookViewModel> Books { get; set; }
        public ICollection<SerieViewModel> Series { get; set; }

        public static AuthorViewModel CreateFromAuthor(Author author,
                                                       bool includeBooks = false,
                                                       bool includeSeries = false)
        {
            var dto = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                Bio = author.Bio,
                CreationDate = author.CreationDate,
                CreatorId = author.CreatorId
            };

            if (includeBooks)
                dto.Books = BookViewModel.CreateFromBooks(author.Books.Select(ab => ab.Book).ToList(), true, true, true);

            if (includeSeries)
                dto.Series = SerieViewModel.CreateFromSeries(author.Series.Select(sa => sa.Serie).ToList(), false, true);

            return dto;
        }

        public static ICollection<AuthorViewModel> CreateFromAuthors(ICollection<Author> authors,
                                                                     bool includeBooks = false,
                                                                     bool includeSeries = false)
        {
            return authors.Select(a => CreateFromAuthor(a, includeBooks, includeSeries)).ToList();
        }
    }
}
