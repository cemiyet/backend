using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Queries.Genres
{
    public class DetailsQueryHandler : IRequestHandler<DetailsQuery, GenreViewModel>
    {
        private readonly AppDataContext _context;

        public DetailsQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<GenreViewModel> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.FindAsync(request.Id);

            if (genre == null)
                throw new GenreNotFoundException(request.Id);

            return GenreViewModel.CreateFromGenre(genre, true);
        }
    }
}
