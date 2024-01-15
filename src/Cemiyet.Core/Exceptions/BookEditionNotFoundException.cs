using System;
using System.Collections.Generic;

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

        public BookEditionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
