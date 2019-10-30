using System;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.DeleteOne
{
    // TODO (v0.1): create validator.
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
