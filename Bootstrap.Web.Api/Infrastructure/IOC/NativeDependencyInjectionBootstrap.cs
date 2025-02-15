using Bootstrap.Interactors.WeatherForecasts.Queries;
using Bootstrap.Web.Api.Filters;
using MediatR;

namespace Bootstrap.Web.Api.Infrastructure.IOC;

public class NativeDependencyInjectionBootstrap
{
	public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
	{
		// services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecast).Assembly));
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		// pipeline order for processing
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehaviour<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
	}
}