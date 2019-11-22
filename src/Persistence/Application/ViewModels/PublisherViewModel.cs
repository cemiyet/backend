using System;
using System.Collections.Generic;
using System.Linq;
using Cemiyet.Core.Entities;

namespace Cemiyet.Persistence.Application.ViewModels
{
    public class PublisherViewModel : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BookEditionViewModel> BookEditions { get; set; }

        public static PublisherViewModel CreateFromPublisher(Publisher publisher, bool includeBookEditions = false)
        {
            var dto = new PublisherViewModel
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Description = publisher.Description,
                CreationDate = publisher.CreationDate,
                CreatorId = publisher.CreatorId
            };

            if (includeBookEditions)
                dto.BookEditions = BookEditionViewModel.CreateFromBookEditions(publisher.BookEditions);

            return dto;
        }

        public static ICollection<PublisherViewModel> CreateFromPublishers(ICollection<Publisher> publisher,
                                                                           bool includeBookEditions = false)
        {
            return publisher.Select(p => CreateFromPublisher(p, includeBookEditions)).ToList();
        }
    }
}
