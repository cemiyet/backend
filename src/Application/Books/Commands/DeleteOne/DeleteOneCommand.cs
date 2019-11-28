using System;
using MediatR;

namespace Cemiyet.Application.Books.Commands.DeleteOne
{
    public class DeleteOneCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
