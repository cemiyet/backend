using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public class BookEditionNotFoundException : NotFoundException
    {
        protected BookEditionNotFoundException() : base()
        {
        }

        public BookEditionNotFoundException(string bookEditionId) : base($"Could not found any edition of book with specified id: {bookEditionId}")
        {
        }

        public BookEditionNotFoundException(IEnumerable<string> bookEditionIds) : base("Could not found any edition of book with specified ids.")
        {
        }

        protected BookEditionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BookEditionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
