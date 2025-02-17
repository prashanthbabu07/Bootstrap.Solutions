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
    public void NotFail_WithCorrectRequest()
    {
        var model = new GetWeatherForecast
        {
            Number = 10
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x);
    }

    [Fact]
    public void NotFailValidations()
    {
        // Arrange
        var model = new GetWeatherForecast();
        // Act
        var result = _validator.TestValidate(model);
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void FailWithValidationError()
    {
        var model = new GetWeatherForecast { Number = 20 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Number)
            .WithErrorMessage("The number should be either null or between 0 and 10");
    }
}