using System.Text;
using System.Text.Json;
using Domain.Common.Validation;
using Domain.Testing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Domain.UnitTests.Common.Validation;

public class ValidationResultTests
{
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void TestValidationResultSerialization()
    {
        var validationResult = new ValidationResult();
        validationResult.AddInfo("a");
        validationResult.AddWarning("b");
        validationResult.AddError("c");
        validationResult.Add(new ValidationMessage(ValidationMessageLevel.Error, "Another Error."));

        var json = JsonSerializer.Serialize(validationResult, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        
        Console.WriteLine(json);

        var deserialized = JsonSerializer.Deserialize<ValidationResult>(json)!;

        deserialized.Should().BeEquivalentTo(validationResult);
        deserialized.Messages.Count.Should().Be(validationResult.Messages.Count);

        deserialized.HasErrors().Should().Be(true);
    }

    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void Merge_ShouldReturnExpectedValidationResult()
    {
        var testObject1 = new ValidationResult();
        testObject1.AddError("Error 1");

        var testObject2 = new ValidationResult();
        testObject2.AddInfo("Info 2");
        testObject2.HasErrors().Should().Be(false);

        testObject2.Merge(testObject1);
        testObject2.HasErrors().Should().Be(true);
        testObject2.Messages.Count.Should().Be(2);
    }

    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void Merge_WithNullShouldReturnExpectedResult()
    {
        var testObject1 = new ValidationResult();
        testObject1.AddError("Error 1");
        
        testObject1.Merge(null);
        testObject1.HasErrors().Should().Be(true);
        testObject1.Messages.Count.Should().Be(1);
    }

    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void ValidationMessage_ToString_ShouldReturnFormatValidationMessage()
    {
        var validationMessage = new ValidationMessage(ValidationMessageLevel.Error, "An Error occured!");

        var actualResult = validationMessage.ToString();

        actualResult.Should().BeEquivalentTo("Error: An Error occured!");
    }
    
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void ValidationResult_ToString_ShouldReturnFormatValidationMessage()
    {
        var validationResult = new ValidationResult();
        validationResult.AddError("An Error occured!");

        var actualResult = validationResult.ToString();

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Error: An Error occured!");
        
        actualResult.Should().BeEquivalentTo(stringBuilder.ToString());

        var validationResult2 = new ValidationResult();
        validationResult2.ToString().Should().Be(string.Empty);
    }

    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void ValidationResult_ThrowOnError_ShouldThrowValidationException()
    {
        var validationResult = new ValidationResult().AddError("Error!");

        Assert.Throws<ValidationException>(() => validationResult.ThrowExceptionOnErrors());
    }
    
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void ValidationResult_ThrowOnError_ShouldNotThrowValidationExceptionWhenResultIsValid()
    {
        var validationResult = new ValidationResult().AddInfo("No Error!");

        Assert.DoesNotThrow(() => validationResult.ThrowExceptionOnErrors());
    }

    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void ValidationException_ConstructorTest()
    {
        var validationException = new ValidationException("Error!");
        validationException.ValidationResult.HasErrors().Should().BeTrue();
    }
}