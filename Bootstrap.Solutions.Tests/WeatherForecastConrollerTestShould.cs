using System.Net;
using System.Text.Json;
using Bootstrap.Interactors.WeatherForecasts;
using Bootstrap.Web.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Bootstrap.Solutions.Tests;

[Collection("API Test Collection")]
public class WeatherForecastConrollerTestShould
{

	private readonly HttpClient _client;

	public WeatherForecastConrollerTestShould(TestHttpClientFixture fixture)
	{
		_client = fixture.Client;
	}


	[Fact]
	public async Task ReturnSuccessAndCorrectContentType_WithValidRequest()
	{
		var response = await _client.GetAsync("/v1/weather-forecast");
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

	[Fact]
	public async Task ReturnNotFound_WhenUsingV2Api()
	{
		var response = await _client.GetAsync("/v2/weather-forecast/default");
		Assert.True(response.StatusCode == HttpStatusCode.NotFound);
	}
}
