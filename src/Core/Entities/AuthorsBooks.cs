using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Join table entity for authors and books.
    /// </summary>
    public class AuthorsBooks
    {
        public Guid AuthorsId { get; set; }
        public Author Author { get; set; }

        public Guid BooksId { get; set; }
        public Book Book { get; set; }
    }
}
