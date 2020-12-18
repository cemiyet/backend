using System;
using System.Runtime.Serialization;

namespace Cemiyet.Core.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException() : base()
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected NotFoundException(string message) : base(message)
        {
        }

        protected NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
