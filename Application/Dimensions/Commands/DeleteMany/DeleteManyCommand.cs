using System;
using MediatR;

namespace Cemiyet.Application.Dimensions.Commands.DeleteMany
{
    // TODO (v0.1): create validator.
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }
}
