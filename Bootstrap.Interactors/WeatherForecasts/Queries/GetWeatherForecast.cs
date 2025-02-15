using System;
using MediatR;

namespace Bootstrap.Interactors.WeatherForecasts.Queries;

public class GetWeatherForecast : IRequest<IEnumerable<WeatherForecast>>
{
}
