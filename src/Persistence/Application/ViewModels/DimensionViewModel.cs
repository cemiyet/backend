using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class DimensionViewModel : Entity<Guid>
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public ICollection<BookEditionViewModel> BookEditions { get; set; }

        public static DimensionViewModel CreateFromDimension(Dimension dimension, bool includeBookEditions = false)
        {
            var dto = new DimensionViewModel
            {
                Id = dimension.Id,
                Width = dimension.Width,
                Height = dimension.Height,
                CreationDate = dimension.CreationDate,
                CreatorId = dimension.CreatorId
            };

            if (includeBookEditions)
                dto.BookEditions = BookEditionViewModel.CreateFromBookEditions(dimension.BookEditions);

            return dto;
        }

        public static ICollection<DimensionViewModel> CreateFromDimensions(ICollection<Dimension> dimensions,
                                                                           bool includeBookEditions = false)
        {
            return dimensions.Select(d => CreateFromDimension(d, includeBookEditions)).ToList();
        }
    }
}
