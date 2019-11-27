using System;
using System.Collections.Generic;
using MediatR;

namespace Cemiyet.Application.Books.Commands.Add
{
    public class AddCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Guid> Genres { get; set; }
        public ICollection<Guid> Authors { get; set; }
    }
}
