using Asp.Versioning;
using Bootstrap.Interactors.WeatherForecasts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Web.Api.Controllers;

[ApiController]
// [Route("v{apiVersion}/[controller]")]
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


    /// <summary>
    /// Retrieves all weather forecasts.
    /// </summary>
    /// <returns>
    // A list of weather forecasts.</returns> 
    [HttpGet("default")]
    public async Task<ActionResult> GetWeatherForecast()
    {
        return Ok(await _mediator.Send(new GetWeatherForecast()));   
    }

    [HttpGet("")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> GetWeatherForecastsV1()
    {
        return Ok(await _mediator.Send(new GetWeatherForecast())); 
    }

    [HttpGet("")]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult> GetWeatherForecastsV2()
    {
        return Ok(await Task.FromResult("hello from v2"));   
    }
}
