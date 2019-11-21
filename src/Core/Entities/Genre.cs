using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Category of literature.
    /// </summary>
    public class Genre : Entity<Guid>
    {
        public string Name { get; set; }

        public ICollection<BooksGenres> Books { get; set; }
    }
}
