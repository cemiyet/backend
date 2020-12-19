using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        protected BookNotFoundException() : base()
        {
        }

        public BookNotFoundException(Guid bookId) : base($"Could not found any book with specified id: {bookId}")
        {
        }

        public BookNotFoundException(IEnumerable<Guid> bookIds) : base("Could not found any book with specified ids.")
        {
        }

        protected BookNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BookNotFoundException(string message) : base(message)
        {
        }

        public BookNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
