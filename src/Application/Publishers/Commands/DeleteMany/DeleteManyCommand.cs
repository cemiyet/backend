using System;
using MediatR;

namespace Cemiyet.Application.Publishers.Commands.DeleteMany
{
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }
}
