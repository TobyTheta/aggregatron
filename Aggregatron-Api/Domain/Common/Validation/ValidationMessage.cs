namespace Domain.Common.Validation;

public readonly record struct ValidationMessage(ValidationMessageLevel Level, string Text,int? EntityId = null, string? Member = null)
{
    public override string ToString()
    {
        return $"{Level}{(Member != null ? " - " + Member : String.Empty)}: {Text}";
    }
}