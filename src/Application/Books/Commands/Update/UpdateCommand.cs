using System;
using MediatR;

namespace Cemiyet.Application.Books.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
