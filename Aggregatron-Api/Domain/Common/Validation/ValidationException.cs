namespace Domain.Common.Validation;

public class ValidationException : Exception
{
    public ValidationResult ValidationResult { get; private set; }

    public ValidationException(ValidationResult result) : base(result.ToString())
    {
        ValidationResult = result;  
    }

    public ValidationException(string error) : this(new ValidationResult().AddError(error))
    {
        
    }
}