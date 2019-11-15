using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;

namespace Cemiyet.Application.Publishers.Queries.Details
{
    public class DetailsHandler : IRequestHandler<DetailsQuery, Publisher>
    {
        private readonly AppDataContext _context;

        public DetailsHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Publisher> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var publisher = await _context.Publishers.FindAsync(request.Id);

            if (publisher == null)
                throw new PublisherNotFoundException(request.Id);

            return publisher;
        }
    }
}
