using Domain.Common.Validation;

namespace Domain.Common.Services;

public record EmptyResponse
{
    public ValidationResult? ValidationResult { get; set; } = null;

    public EmptyResponse()
    {
        
    }
    
    public EmptyResponse(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }

    public EmptyResponse(string validationError) : this(new ValidationResult().AddError(validationError))
    {
        
    }
}