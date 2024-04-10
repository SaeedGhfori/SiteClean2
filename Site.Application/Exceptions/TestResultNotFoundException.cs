namespace Site.Application.Exceptions
{
    public class TestResultNotFoundException : BusinessViolatedException
    {
        public TestResultNotFoundException() : base("Test Result Not Found")
        {
            BusinessRule = BusinessRule.EntityNotFound;
        }
    }
}