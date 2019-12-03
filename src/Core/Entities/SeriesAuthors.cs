using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Join table entity for series and authors.
    /// </summary>
    public class SeriesAuthors
    {
        public Guid SeriesId { get; set; }
        public virtual Serie Serie { get; set; }

        public Guid AuthorsId { get; set; }
        public virtual Author Author { get; set; }
    }
}
