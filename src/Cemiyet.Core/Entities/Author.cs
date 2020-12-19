using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Author of literature.
    /// </summary>
    public class Author : Entity<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<AuthorsBooks> Books { get; set; }
    }
}
