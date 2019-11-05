using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public class GenreNotFoundException : Exception
    {
        public GenreNotFoundException(Guid genreId) : base($"Could not found any genre with specified id: {genreId}")
        {
        }

        public GenreNotFoundException(IEnumerable<Guid> genreId) : base("Could not found any genre with specified ids.")
        {
        }

        protected GenreNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public GenreNotFoundException(string message) : base(message)
        {
        }

        public GenreNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
