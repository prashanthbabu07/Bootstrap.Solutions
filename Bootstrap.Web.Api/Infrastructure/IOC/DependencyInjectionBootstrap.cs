using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bootstrap.Data.Services;
using Bootstrap.Interactors.WeatherForecasts;
using Bootstrap.Web.Api.Filters;
using FluentValidation;
using MediatR;

namespace Bootstrap.Web.Api.Infrastructure.IOC;

public class DependencyInjectionBootstrap
{
	private const string _service = "Service";

	public static void RegisterServices(WebApplicationBuilder builder, IConfiguration configuration)
	{
		builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecast).Assembly));
		builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

		builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		// pipeline order for processing
		builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehaviour<,>));
		builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

		builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(cb =>
		{
			// Register your services, modules, etc. with containerBuilder
			cb.RegisterAssemblyTypes(typeof(GetWeatherForecast).GetTypeInfo().Assembly)
				.AsClosedTypesOf(typeof(AbstractValidator<>))
				.AsImplementedInterfaces()
				.InstancePerDependency();

			cb.RegisterAssemblyTypes(typeof(IWeatherDataService).GetTypeInfo().Assembly)
			.Where(t => t.Name.EndsWith(_service))
			.AsImplementedInterfaces()
			.InstancePerDependency();
		}));
	}
}
