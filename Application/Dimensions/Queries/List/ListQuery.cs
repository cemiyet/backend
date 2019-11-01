using System.Collections.Generic;
using Cemiyet.Core.Entities;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Dimensions.Queries.List
{
    public class ListQuery : PageableModel, IRequest<List<Dimension>>
    {
    }
}
