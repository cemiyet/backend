using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException(Guid authorId) : base($"Could not found any author with specified id: {authorId}")
        {
        }

        public AuthorNotFoundException(IEnumerable<Guid> authorIds) : base("Could not found any author with specified ids.")
        {
        }

        protected AuthorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
