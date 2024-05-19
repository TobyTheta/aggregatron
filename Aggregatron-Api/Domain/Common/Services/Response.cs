using Domain.Common.Validation;

namespace Domain.Common.Services;

public record Response<T> where T : class?
{
    public Response()
    {
        
    }

    public Response(T data, ValidationResult? validationResult = null)
    {
        Data = data;
        ValidationResult = validationResult;
    }

    public Response(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }

    public Response(string validationError) : this(new ValidationResult().AddError(validationError))
    {
        
    }
    
    public T? Data { get; init; } = null;

    public ValidationResult? ValidationResult { get; init; } = null;
}