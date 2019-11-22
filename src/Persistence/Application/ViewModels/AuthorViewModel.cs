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

        public static AuthorViewModel CreateFromAuthor(Author author, bool includeBooks = false)
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
                dto.Books = BookViewModel.CreateFromBooks(author.Books.Select(gb => gb.Book).ToList(),
                                                          true, true, true);

            return dto;
        }

        public static ICollection<AuthorViewModel> CreateFromAuthors(ICollection<Author> authors,
                                                                     bool includeBooks = false)
        {
            return authors.Select(b => CreateFromAuthor(b, includeBooks)).ToList();
        }
    }
}
