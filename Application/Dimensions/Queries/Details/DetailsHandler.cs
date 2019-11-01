using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Contexts;
using MediatR;

namespace Cemiyet.Application.Dimensions.Queries.Details
{
    public class DetailsHandler : IRequestHandler<DetailsQuery, Dimension>
    {
        private readonly MainDataContext _context;

        public DetailsHandler(MainDataContext context)
        {
            _context = context;
        }

        public async Task<Dimension> Handle(DetailsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Dimensions.FindAsync(request.Id);
        }
    }
}
