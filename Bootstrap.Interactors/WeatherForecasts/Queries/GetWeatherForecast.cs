using System;
using MediatR;

namespace Bootstrap.Interactors.WeatherForecasts.Queries;

public class GetWeatherForecast : IRequest<IEnumerable<WeatherForecast>>
{
	/// <summary>
	/// Number should be between 0 and 10
	/// </summary>
	/// <value></value>
	public int? Number { get; set; }
}
