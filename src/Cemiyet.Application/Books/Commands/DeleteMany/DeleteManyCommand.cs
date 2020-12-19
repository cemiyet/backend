using System;
using MediatR;

namespace Cemiyet.Application.Books.Commands.DeleteMany
{
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }
}
