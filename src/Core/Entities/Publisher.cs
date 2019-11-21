using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Publisher of literature.
    /// </summary>
    public class Publisher : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BookEdition> BookEditions { get; set; }
    }
}
