using System;
using MediatR;

namespace Cemiyet.Application.Publishers.Commands.DeleteOne
{
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
