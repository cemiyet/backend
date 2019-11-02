using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.Details
{
    public class DetailsHandler : IRequestHandler<DetailsQuery, Genre>
    {
        private readonly MainDataContext _context;

        public DetailsHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Genre> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Genres.FindAsync(request.Id);
        }
    }
}
