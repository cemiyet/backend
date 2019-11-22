using System;

namespace Cemiyet.Core.Entities
{
    /// <summary>
    /// Editions of book.
    /// </summary>
    public class BookEdition
    {
        public string Isbn { get; set; }
        public short PageCount { get; set; }
        public DateTime PrintDate { get; set; }
        public DateTime CreationDate { get; set; }

        public Guid PublishersId { get; set; }
        public virtual Publisher Publisher { get; set; }

        public Guid BooksId { get; set; }
        public virtual Book Book { get; set; }

        public Guid DimensionsId { get; set; }
        public virtual Dimension Dimensions { get; set; }

        // TODO (v0.5): create relations with user model.
        public Guid CreatorId { get; set; }
        // public User Creator { get; set; }
    }
}
