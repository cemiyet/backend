using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class BookEditionViewModel
    {
        public string Isbn { get; set; }
        public short PageCount { get; set; }
        public DateTime PrintDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatorId { get; set; }

        public BookViewModel Book { get; set; }
        public DimensionViewModel Dimensions { get; set; }
        public PublisherViewModel Publisher { get; set; }

        public static BookEditionViewModel CreateFromBookEdition(BookEdition bookEdition,
                                                                 bool includeBook = false,
                                                                 bool includeDimensions = false,
                                                                 bool includePublisher = false)
        {
            var dto = new BookEditionViewModel
            {
                Isbn = bookEdition.Isbn,
                PageCount = bookEdition.PageCount,
                PrintDate = bookEdition.PrintDate,
                CreationDate = bookEdition.CreationDate,
                CreatorId = bookEdition.CreatorId
            };

            if (includeBook)
                dto.Book = BookViewModel.CreateFromBook(bookEdition.Book);

            if (includeDimensions)
                dto.Dimensions = DimensionViewModel.CreateFromDimension(bookEdition.Dimensions);

            if (includePublisher)
                dto.Publisher = PublisherViewModel.CreateFromPublisher(bookEdition.Publisher);

            return dto;
        }

        public static ICollection<BookEditionViewModel> CreateFromBookEditions(IEnumerable<BookEdition> bookEdition,
                                                                               bool includeBook = false,
                                                                               bool includeDimensions = false,
                                                                               bool includePublisher = false)
        {
            return bookEdition.Select(p => CreateFromBookEdition(p, includeBook, includeDimensions, includePublisher))
                              .ToList();
        }
    }
}
