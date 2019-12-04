using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class SerieBookViewModel
    {
        public short Order { get; set; }
        public BookViewModel Book { get; set; }

        public static SerieBookViewModel CreateFromSerieBook(SeriesBooks book)
        {
            var dto = new SerieBookViewModel
            {
                Order = book.Order,
                Book = BookViewModel.CreateFromBook(book.Book)
            };

            return dto;
        }

        public static ICollection<SerieBookViewModel> CreateFromSeriesBooks(ICollection<SeriesBooks> books)
        {
            return books.Select(sb => CreateFromSerieBook(sb)).ToList();
        }
    }
}
