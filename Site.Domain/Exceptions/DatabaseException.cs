using System;

namespace Site.Domain.Exceptions
{
    public class DatabaseException : Exception
    {
        public ErrorType RuleId { get; set; }
        public DatabaseException(ErrorType ruleId)
        {
            RuleId = ruleId;
        }
    }
}