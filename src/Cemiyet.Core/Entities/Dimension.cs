using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Physical dimensions of book.
    /// </summary>
    public class Dimension : Entity<Guid>
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public virtual ICollection<BookEdition> BookEditions { get; set; }
    }
}
