using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Join table entity for books and genres.
    /// </summary>
    public class BooksGenres
    {
        public Guid BooksId { get; set; }
        public virtual Book Book { get; set; }

        public Guid GenresId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
