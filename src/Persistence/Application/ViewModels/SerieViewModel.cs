using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Extensions;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class SerieViewModel : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<AuthorViewModel> Authors { get; set; }
        public ICollection<SerieBookViewModel> Books { get; set; }

        public static SerieViewModel CreateFromSerie(Serie serie,
                                                     bool includeAuthors = false,
                                                     bool includeBooks = false)
        {
            var dto = new SerieViewModel
            {
                Id = serie.Id,
                Title = serie.Title,
                Description = serie.Description,
                CreationDate = serie.CreationDate,
                CreatorId = serie.CreatorId
            };

            if (includeAuthors)
                dto.Authors = AuthorViewModel.CreateFromAuthors(serie.Books.GetAuthors());

            if (includeBooks)
                dto.Books = SerieBookViewModel.CreateFromSeriesBooks(serie.Books);

            return dto;
        }

        public static ICollection<SerieViewModel> CreateFromSeries(IEnumerable<Serie> series,
                                                                   bool includeAuthors = false,
                                                                   bool includeBooks = false)
        {
            return series.Select(s => CreateFromSerie(s, includeAuthors, includeBooks)).ToList();
        }
    }
}
