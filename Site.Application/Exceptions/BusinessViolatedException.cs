using System;
using System.Runtime.Serialization;

namespace Site.Application.Exceptions
{
    public class BusinessViolatedException : ApplicationException
    {
        public BusinessRule BusinessRule { get; protected set; }
        public BusinessViolatedException()
        {

        }
        public BusinessViolatedException(string message, BusinessRule businessRule) : base(message)
        {
            BusinessRule = businessRule;
        }

        public BusinessViolatedException(string message) : base(message)
        {
        }

        public BusinessViolatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessViolatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}