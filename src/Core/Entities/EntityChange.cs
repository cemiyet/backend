using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Provides identifiers for EntityChange entity.
    /// </summary>
    /// <typeparam name="T">Type of the identifiers. Should be same with all entities.</typeparam>
    public abstract class BaseEntityChange<T>
    {
        public T Id { get; set; }

        public T EntityId { get; set; }

        // TODO (v0.5): create relations with user model.
        public T ModifierId { get; set; }
    }

    /// <summary>
    /// Basic data for entity change tracking.
    /// </summary>
    public class EntityChange : BaseEntityChange<Guid>
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
