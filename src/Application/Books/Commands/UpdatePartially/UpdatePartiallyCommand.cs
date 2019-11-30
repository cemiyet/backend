using System;
using MediatR;

namespace Cemiyet.Application.Books.Commands.UpdatePartially
{
    public class UpdatePartiallyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
