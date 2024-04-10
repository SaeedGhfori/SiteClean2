using System;

namespace Site.Domain.Exceptions
{
    public class MyArgumentNullException : ArgumentNullException
    {
        public ErrorType RuleId { get; set; }
        public MyArgumentNullException(ErrorType rule)
        {
            RuleId = rule;
        }
    }
}