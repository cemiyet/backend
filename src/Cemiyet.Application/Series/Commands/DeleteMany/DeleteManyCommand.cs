using System;
using MediatR;

namespace Cemiyet.Application.Series.Commands.DeleteMany
{
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }
}
