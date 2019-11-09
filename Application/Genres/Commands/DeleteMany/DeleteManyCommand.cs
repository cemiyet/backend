using System;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.DeleteMany
{
    public class DeleteManyCommand : IRequest
    {
        public Guid[] Ids { get; set; }
    }
}
