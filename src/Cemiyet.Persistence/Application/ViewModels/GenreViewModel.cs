using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class GenreViewModel : Entity<Guid>
    {
        public string Name { get; set; }

        public ICollection<BookViewModel> Books { get; set; }

        public static GenreViewModel CreateFromGenre(Genre genre, bool includeBooks = false)
        {
            var dto = new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                CreationDate = genre.CreationDate,
                CreatorId = genre.CreatorId
            };

            if (includeBooks)
                dto.Books = BookViewModel.CreateFromBooks(genre.Books.Select(gb => gb.Book).ToList(), true, true, true);

            return dto;
        }

        public static ICollection<GenreViewModel> CreateFromGenres(IEnumerable<Genre> genres, bool includeBooks = false)
        {
            return genres.Select(g => CreateFromGenre(g, includeBooks)).ToList();
        }
    }
}
