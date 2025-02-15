using System;
using MediatR;

namespace Bootstrap.Interactors.WeatherForecasts.Queries;

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecast, IEnumerable<WeatherForecast>>
{
	private static readonly string[] Summaries = new[]
   {
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecast request, CancellationToken cancellationToken)
	{
		return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}));
	}
}
