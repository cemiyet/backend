using System;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.Update
{
    // TODO (v0.1): create validator.
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
