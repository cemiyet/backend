using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Publishers
{
    public class ListQueryHandler : IRequestHandler<ListQuery, List<PublisherViewModel>>
    {
        private readonly AppDataContext _context;

        public ListQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<PublisherViewModel>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var publisherSet = await _context.Publishers.PagedToListAsync(request.Page, request.PageSize);
            return PublisherViewModel.CreateFromPublishers(publisherSet).ToList();
        }
    }
}
