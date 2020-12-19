using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Queries.Series
{
    public class DetailsQueryHandler : IRequestHandler<DetailsQuery, SerieViewModel>
    {
        private readonly AppDataContext _context;

        public DetailsQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<SerieViewModel> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var serie = await _context.Series.FindAsync(request.Id);

            if (serie == null)
                throw new SerieNotFoundException(request.Id);

            return SerieViewModel.CreateFromSerie(serie, true, true);
        }
    }
}
