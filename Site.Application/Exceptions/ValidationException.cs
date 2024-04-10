using FluentValidation.Results;

namespace Site.Application.Exceptions
{
    public class ValidationExceptionApp : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationExceptionApp(ValidationResult validationResult)
        {

            foreach (var err in validationResult.Errors)
            {
                Errors.Add(err.ErrorMessage);
            }
        }
    }
}
