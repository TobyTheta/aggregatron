using Domain.Common.Services;
using Domain.Common.Validation;
using Domain.Testing;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.UnitTests.Common.Services;

public class ResponseTests
{
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void ResponseConstructorTests()
    {
        var response = new Response<string>()
        {
            Data = "Test",
            ValidationResult = new ValidationResult(),
        };

        response.Data.Should().Be("Test");
        response.ValidationResult.Should().NotBeNull();
        
        response = new Response<string>("Test", new ValidationResult());

        response.Data.Should().Be("Test");
        response.ValidationResult.Should().NotBeNull();
        
        response = new Response<string>(new ValidationResult());
        response.ValidationResult.Should().NotBeNull();

        response = new Response<string>(validationError: "Error!");
        response.ValidationResult.Should().NotBeNull();
        response.ValidationResult!.HasErrors().Should().BeTrue();
    }
    
    [Test]
    [Category(TestCategory.ContinuousIntegration)]
    public void EmptyResponseConstructorTests()
    {
        var response = new EmptyResponse()
        {
            ValidationResult = new ValidationResult(),
        };

        response.ValidationResult.Should().NotBeNull();
        
        response = new EmptyResponse(new ValidationResult());

        response.ValidationResult.Should().NotBeNull();
        
        response = new EmptyResponse(validationError: "Error!");
        response.ValidationResult.Should().NotBeNull();
        response.ValidationResult!.HasErrors().Should().BeTrue();
    }
}