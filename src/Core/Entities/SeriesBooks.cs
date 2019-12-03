using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Join table entity for series and books.
    /// </summary>
    public class SeriesBooks
    {
        public Guid SeriesId { get; set; }
        public virtual Serie Serie { get; set; }

        public Guid BooksId { get; set; }
        public virtual Book Book { get; set; }
    }
}
