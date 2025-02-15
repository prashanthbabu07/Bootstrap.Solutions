using Bootstrap.Interactors.WeatherForecasts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
	private readonly IMediator _mediator;

	public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult> GetWeatherForecast()
    {
        return Ok(await _mediator.Send(new GetWeatherForecast()));   
    }
}
