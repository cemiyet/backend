using System.Collections.Generic;
using Cemiyet.Persistence.Application.ViewModels;
using Cemiyet.Persistence.Extensions;
using MediatR;

namespace Cemiyet.Application.Dimensions.Queries.List
{
    public class ListQuery : PageableModel, IRequest<List<DimensionViewModel>>
    {
    }
}
