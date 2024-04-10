using System;

namespace Site.Domain.Exceptions
{
    public class NetworkException : Exception
    {
        public ErrorType RuleId { get; set; }
        public NetworkException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }
    }
}