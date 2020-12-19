using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public class SerieNotFoundException : NotFoundException
    {
        protected SerieNotFoundException() : base()
        {
        }

        public SerieNotFoundException(Guid serieId) : base($"Could not found any serie with specified id: {serieId}")
        {
        }

        public SerieNotFoundException(IEnumerable<Guid> serieIds) : base("Could not found any series with specified ids.")
        {
        }

        protected SerieNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SerieNotFoundException(string message) : base(message)
        {
        }

        public SerieNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
