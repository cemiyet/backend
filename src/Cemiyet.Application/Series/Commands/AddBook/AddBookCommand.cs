using System;
using System.Collections.Generic;
using MediatR;

namespace Cemiyet.Application.Series.Commands.AddBook
{
    public class AddBookCommand : IRequest
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, short> Books { get; set; }
    }
}
