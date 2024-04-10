using System;
using System.Runtime.Serialization;

namespace Site.Application.Exceptions
{
    public class IntegrationException : Exception
    {
        public IntegrationException()
        {
        }

        public IntegrationException(string message) : base(message)
        {
        }

        public IntegrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IntegrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IntegrationException(object data) : base()
        {
        }
    }
}