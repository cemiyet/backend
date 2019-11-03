using System;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.DeleteOne
{
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
