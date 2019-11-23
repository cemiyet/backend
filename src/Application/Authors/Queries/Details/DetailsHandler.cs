using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Authors.Queries.Details
{
    public class DetailsHandler : IRequestHandler<DetailsQuery, AuthorViewModel>
    {
        private readonly AppDataContext _context;

        public DetailsHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<AuthorViewModel> Handle(DetailsQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new AuthorNotFoundException(request.Id);

            return AuthorViewModel.CreateFromAuthor(author);
        }
    }
}
