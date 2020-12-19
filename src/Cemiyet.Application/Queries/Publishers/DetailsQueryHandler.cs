using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Queries.Publishers
{
    public class DetailsQueryHandler : IRequestHandler<DetailsQuery, PublisherViewModel>
    {
        private readonly AppDataContext _context;

        public DetailsQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<PublisherViewModel> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var publisher = await _context.Publishers.FindAsync(request.Id);

            if (publisher == null)
                throw new PublisherNotFoundException(request.Id);

            return PublisherViewModel.CreateFromPublisher(publisher);
        }
    }
}
