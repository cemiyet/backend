using System.Collections.Generic;
using Cemiyet.Core.Entities;
using MediatR;

namespace Cemiyet.Application.Genres.Queries.List
{
    // TODO: create validator.
    public class ListQuery : IRequest<List<Genre>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
