using System;
using MediatR;

namespace Cemiyet.Application.Publishers.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
