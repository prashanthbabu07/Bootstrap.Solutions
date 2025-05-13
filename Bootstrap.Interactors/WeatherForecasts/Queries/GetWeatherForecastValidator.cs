using System;
using FluentValidation;

namespace Bootstrap.Interactors.WeatherForecasts;

public class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecast>
{
	public GetWeatherForecastValidator()
	{
		RuleFor(x => x.Number)
			.Must(number => number == null || (number >= 0 && number <= 10))
			.WithMessage("The number should be either null or between 0 and 10");
	}
}
