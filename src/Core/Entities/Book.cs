using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Product of literature.
    /// </summary>
    public class Book : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<BookEdition> Editions { get; set; }
        public ICollection<BooksGenres> Genres { get; set; }
        public ICollection<AuthorsBooks> Authors { get; set; }
    }
}
