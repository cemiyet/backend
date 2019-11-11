using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Generic base class for the entities.
    /// </summary>
    /// <typeparam name="T">Type for the identifier. Must be same with the Id of the User model.</typeparam>
    public abstract class Entity<T>
    {
        public T Id { get; set; }
        public DateTime CreationDate { get; set; }

        // TODO (v0.4): create relations with user model.
        public T CreatorId { get; set; }
    }
}
