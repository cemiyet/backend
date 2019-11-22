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

        public virtual ICollection<BooksGenres> Genres { get; set; }
        public virtual ICollection<AuthorsBooks> Authors { get; set; }
        public virtual ICollection<BookEdition> Editions { get; set; }
    }
}
