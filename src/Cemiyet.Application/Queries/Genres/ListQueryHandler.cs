using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Queries.Genres
{
    public class ListQueryHandler : IRequestHandler<ListQuery, List<GenreViewModel>>
    {
        private readonly AppDataContext _context;

        public ListQueryHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<GenreViewModel>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var genreSet = await _context.Genres.PagedToListAsync(request.Page, request.PageSize);

            return GenreViewModel.CreateFromGenres(genreSet).ToList();
        }
    }
}
