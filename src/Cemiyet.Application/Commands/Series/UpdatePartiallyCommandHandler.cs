using System;
using System.Threading;
using System.Threading.Tasks;
using Cemiyet.Core.Exceptions;
using Cemiyet.Persistence.Application.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cemiyet.Application.Commands.Series
{
    public class UpdatePartiallyCommandHandler : IRequestHandler<UpdatePartiallyCommand>
    {
        private readonly AppDataContext _context;

        public UpdatePartiallyCommandHandler(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePartiallyCommand request, CancellationToken cancellationToken)
        {
            var serie = await _context.Series.FindAsync(request.Id);
            if (serie == null) throw new SerieNotFoundException(request.Id);

            if (!string.IsNullOrEmpty(request.Title) && request.Title != serie.Title)
                serie.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Description) && request.Description != serie.Description)
                serie.Description = request.Description;

            if (_context.Entry(serie).State != EntityState.Modified)
                throw new Exception("Nothing updated.");

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (success) return Unit.Value;

            throw new Exception("Problem saving changes.");
        }
    }
}
