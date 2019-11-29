using System;
using MediatR;

namespace Cemiyet.Application.Books.Commands.UpdateEdition
{
    public class UpdateEditionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
        public short PageCount { get; set; }
        public DateTime PrintDate { get; set; }

        public Guid BooksId { get; set; }
        public Guid DimensionsId { get; set; }
        public Guid PublishersId { get; set; }
    }
}
