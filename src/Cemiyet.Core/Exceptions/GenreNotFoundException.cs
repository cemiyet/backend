using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Exceptions
{
    public class GenreNotFoundException : NotFoundException
    {
        protected GenreNotFoundException() : base()
        {
        }

        public GenreNotFoundException(Guid genreId) : base($"Could not found any genre with specified id: {genreId}")
        {
        }

        public GenreNotFoundException(IEnumerable<Guid> genreIds) : base("Could not found any genre with specified ids.")
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
