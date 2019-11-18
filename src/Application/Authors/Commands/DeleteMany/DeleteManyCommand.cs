using System;
using MediatR;

namespace Cemiyet.Application.Authors.Commands.DeleteMany
{
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }
}
