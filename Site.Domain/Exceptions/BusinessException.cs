using System;

namespace Site.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public ErrorType RuleId { get; set; }
        public BusinessException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }

    }
}


