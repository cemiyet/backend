using System;
using System.Collections.Generic;

namespace Cemiyet.Core.Exceptions
{
    public class PublisherNotFoundException : NotFoundException
    {
        protected PublisherNotFoundException() : base()
        {
        }

        public PublisherNotFoundException(Guid publisherId) : base($"Could not found any publisher with specified id: {publisherId}")
        {
        }

        public PublisherNotFoundException(IEnumerable<Guid> publisherIds) : base("Could not found any publisher with specified ids.")
        {
        }

        public PublisherNotFoundException(string message) : base(message)
        {
        }

        public PublisherNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
