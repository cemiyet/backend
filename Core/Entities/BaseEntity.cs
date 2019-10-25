using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Generic base class for the entities.
    /// </summary>
    /// <typeparam name="TId">Type for the identifier. Must be same with the Id of the User model.</typeparam>
    public class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public TId CreatorId { get; set; }
        public TId ModifierId { get; set; }
    }
}
