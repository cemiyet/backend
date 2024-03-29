using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Queries.Dimensions
{
    public class DetailsQueryHandler : IRequestHandler<DetailsQuery, DimensionViewModel>
    {
        private readonly AppDataContext _context;

        public DetailsQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<DimensionViewModel> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var dimension = await _context.Dimensions.FindAsync(request.Id);

            if (dimension == null)
                throw new DimensionNotFoundException(request.Id);

            return DimensionViewModel.CreateFromDimension(dimension);
        }
    }
}
