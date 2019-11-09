using System;
using MediatR;

namespace Cemiyet.Application.Genres.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
