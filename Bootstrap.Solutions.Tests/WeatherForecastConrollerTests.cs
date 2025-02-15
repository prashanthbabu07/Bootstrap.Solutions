using System.Text.Json;
using Bootstrap.Interactors.WeatherForecasts.Queries;
using Bootstrap.Web.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Bootstrap.Solutions.Tests;

[Collection("API Test Collection")]
public class WeatherForecastConrollerTests
{

	private readonly HttpClient _client;

	public WeatherForecastConrollerTests(TestHttpClientFixture fixture)
	{
		_client = fixture.Client;
	}


	[Fact]
	public async Task GetWeatherForecast_ReturnsSuccessAndCorrectContentType()
	{
		var response = await _client.GetAsync("/WeatherForecast");
		response.EnsureSuccessStatusCode();

		Assert.Equal("application/json; charset=utf-8",
			response.Content.Headers.ContentType?.ToString());

		var content = await response.Content.ReadAsStringAsync();
		var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(forecasts);
		Assert.NotEmpty(forecasts);
	}
}
