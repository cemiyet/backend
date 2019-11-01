using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Generic base class for the entities.
    /// </summary>
    /// <typeparam name="TId">Type for the identifier. Must be same with the Id of the User model.</typeparam>
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        // TODO (v0.4): create relations with user model.
        public TId CreatorId { get; set; }
        public TId ModifierId { get; set; }
    }
}
