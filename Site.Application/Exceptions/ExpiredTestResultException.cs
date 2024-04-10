namespace Site.Application.Exceptions
{
    public class ExpiredTestResultException : BusinessViolatedException
    {
        public ExpiredTestResultException() : base("Test result is expired!")
        {
            BusinessRule = BusinessRule.ExpiredTestResult;
        }
    }
}