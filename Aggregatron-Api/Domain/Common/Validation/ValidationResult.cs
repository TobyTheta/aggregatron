using System.Text;

namespace Domain.Common.Validation;

public record ValidationResult
{
    public List<ValidationMessage> Messages { get; set; }

    public ValidationResult() : this(new List<ValidationMessage>())
    {
        
    }
    
    public ValidationResult(List<ValidationMessage>? validationMessages)
    {
        Messages = validationMessages ?? new List<ValidationMessage>();
    }
    
    public void Add(ValidationMessage message)
    {
        Messages.Add(message);
    }

    public ValidationResult AddError(string errorText, string? member = null)
    {
        Messages.Add(new ValidationMessage(ValidationMessageLevel.Error, errorText, member));
        return this;
    }

    public ValidationResult AddWarning(string warningText, string? member = null)
    {
        Messages.Add(new ValidationMessage(ValidationMessageLevel.Warning, warningText, member));
        return this;
    }

    public ValidationResult AddInfo(string infoText, string? member = null)
    {
        Messages.Add(new ValidationMessage(ValidationMessageLevel.Info, infoText, member));
        return this;
    }

    public bool HasErrors()
    {
        return Messages.Any(x => x.Level == ValidationMessageLevel.Error);
    }

    public void Merge(ValidationResult? other)
    {
        if(other == null)
        {
            return;
        }

        Messages.AddRange(other.Messages);
    }

    public override string ToString()
    {
        if(Messages.Count == 0)
        {
            return String.Empty;
        }

        var stringBuilder = new StringBuilder();
        foreach(var message in Messages)
        {
            stringBuilder.AppendLine(message.ToString());
        }

        return stringBuilder.ToString();
    }

    public void ThrowExceptionOnErrors()
    {
        var errors = Messages.Where(x => x.Level == ValidationMessageLevel.Error).ToList();
        if (!errors.Any())
        {
            return;
        }

        throw new ValidationException(this);
    }
};