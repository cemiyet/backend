using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public class PublisherNotFoundException : NotFoundException
    {
        public PublisherNotFoundException(Guid publisherId) : base($"Could not found any publisher with specified id: {publisherId}")
        {
        }

        public PublisherNotFoundException(IEnumerable<Guid> publisherIds) : base("Could not found any publisher with specified ids.")
        {
        }

        protected PublisherNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
