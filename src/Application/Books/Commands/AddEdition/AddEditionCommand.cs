using System;
using MediatR;

namespace Cemiyet.Application.Books.Commands.AddEdition
{
    public class AddEditionCommand : IRequest
    {
        public string Isbn { get; set; }
        public short PageCount { get; set; }
        public DateTime PrintDate { get; set; }

        public Guid BooksId { get; set; }
        public Guid DimensionsId { get; set; }
        public Guid PublishersId { get; set; }
    }
}
