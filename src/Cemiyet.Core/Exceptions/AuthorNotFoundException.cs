using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Exceptions
{
    public class AuthorNotFoundException : NotFoundException
    {
        protected AuthorNotFoundException() : base()
        {
        }

        public AuthorNotFoundException(Guid authorId) : base($"Could not found any author with specified id: {authorId}")
        {
        }

        public AuthorNotFoundException(IEnumerable<Guid> authorIds) : base("Could not found any author with specified ids.")
        {
        }

        public AuthorNotFoundException(string message) : base(message)
        {
        }

        public AuthorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
