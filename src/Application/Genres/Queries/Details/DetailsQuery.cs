using System;
using Cemiyet.Persistence.Application.ViewModels;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.Details
{
    public class DetailsQuery : IRequest<GenreViewModel>
    {
        public Guid Id { get; set; }
    }
}
