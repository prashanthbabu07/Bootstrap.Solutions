using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;
using Bootstrap.Interactors.WeatherForecasts.Queries; 

namespace Bootstrap.Solutions.Tests;

public class GetWeatherForecastValidatorTestShould
{
    private readonly GetWeatherForecastValidator _validator;

    public GetWeatherForecastValidatorTestShould()
    {
        _validator = new GetWeatherForecastValidator();
    }

    [Fact]
    public void Validation_Should_NotFail_With_Correct_Message()
    {
        // Arrange
        // var model = new GetWeatherForecast(); // Create an instance of your model.
        // // Act
        // var result = _validator.TestValidate(model);
        // // Assert
        // result.ShouldHaveValidationErrorFor(x => x)
        //       .WithErrorMessage("The value must be 1");

        var model = new GetWeatherForecast();
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x);
    }

    [Fact]
    public void Validation_Should_NotFail_Always()
    {
        // Arrange
        var model = new GetWeatherForecast();
        // Act
        var result = _validator.TestValidate(model);
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}