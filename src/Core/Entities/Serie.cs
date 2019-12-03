using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Series of literature.
    /// </summary>
    public class Serie : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SeriesAuthors> Authors { get; set; }
        public virtual ICollection<SeriesBooks> Books { get; set; }
    }
}
