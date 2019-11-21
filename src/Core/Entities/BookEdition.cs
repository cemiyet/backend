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
        public Publisher Publisher { get; set; }

        public Guid BooksId { get; set; }
        public Book Book { get; set; }

        public Guid DimensionsId { get; set; }
        public Dimension Dimensions { get; set; }

        // TODO (v0.4): create relations with user model.
        public Guid CreatorId { get; set; }
        // public User Creator { get; set; }
    }
}
