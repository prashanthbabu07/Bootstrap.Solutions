using System;
using FluentValidation;

namespace Bootstrap.Interactors.WeatherForecasts.Queries;

public class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecast>
{
	public GetWeatherForecastValidator()
	{
		RuleFor(x => x).Must(x => 1 == 1).WithMessage("The value must be 1");
	}
}
