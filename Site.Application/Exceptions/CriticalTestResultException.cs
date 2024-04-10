namespace Site.Application.Exceptions
{
    public class CriticalTestResultException : BusinessViolatedException
    {
        public CriticalTestResultException() : base("Test result contains critical answers")
        {
            BusinessRule = BusinessRule.CriticalTestResult;
        }
    }
}