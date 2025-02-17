using Asp.Versioning;
using Bootstrap.Interactors.WeatherForecasts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Web.Api.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[Route("v{version:apiVersion}/weather-forecast")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
	private readonly IMediator _mediator;

	public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("default")]
    public async Task<ActionResult> GetWeatherForecast()
    {
        return Ok(await _mediator.Send(new GetWeatherForecast()));   
    }

    [HttpGet("")]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult> GetWeatherForecastsV2()
    {
        return Ok(await _mediator.Send(new GetWeatherForecast()));   
    }
}
