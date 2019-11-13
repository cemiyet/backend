using System;

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
    }
}
