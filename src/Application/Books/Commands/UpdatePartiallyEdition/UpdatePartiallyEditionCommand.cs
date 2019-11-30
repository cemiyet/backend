using System;
using MediatR;

namespace Cemiyet.Application.Books.Commands.UpdatePartiallyEdition
{
    public class UpdatePartiallyEditionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
        public string NewIsbn { get; set; }
        public short PageCount { get; set; }
        public DateTime PrintDate { get; set; }

        public Guid BooksId { get; set; }
        public Guid DimensionsId { get; set; }
        public Guid PublishersId { get; set; }
    }
}
