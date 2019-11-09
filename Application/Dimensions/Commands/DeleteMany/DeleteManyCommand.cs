using System;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.DeleteMany
{
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }
}
