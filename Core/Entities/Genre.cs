using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Category of literature.
    /// </summary>
    public class Genre : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
