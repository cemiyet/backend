using System;
using MediatR;

namespace Cemiyet.Application.Publishers.Commands.UpdatePartially
{
    public class UpdatePartiallyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
