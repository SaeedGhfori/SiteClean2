using System;
using System.Runtime.Serialization;

namespace Site.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public ErrorType RuleId { get; set; }
        public EntityNotFoundException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }
    }

}
