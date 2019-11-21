using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Join table entity for books and genres.
    /// </summary>
    public class BooksGenres
    {
        public Guid BooksId { get; set; }
        public Book Book { get; set; }

        public Guid GenresId { get; set; }
        public Genre Genre { get; set; }
    }
}
