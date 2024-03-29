using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public class DimensionNotFoundException : NotFoundException
    {
        protected DimensionNotFoundException() : base()
        {
        }

        public DimensionNotFoundException(Guid dimensionId) : base($"Could not found any dimension with specified id: {dimensionId}")
        {
        }

        public DimensionNotFoundException(IEnumerable<Guid> dimensionIds) : base("Could not found any dimension with specified ids.")
        {
        }

        protected DimensionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DimensionNotFoundException(string message) : base(message)
        {
        }

        public DimensionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
