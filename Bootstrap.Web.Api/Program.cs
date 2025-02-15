using Autofac.Extensions.DependencyInjection;
using Bootstrap.Web.Api.Filters;
using Bootstrap.Web.Api.Infrastructure.IOC;

namespace Bootstrap.Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // swagger ui
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        DependencyInjectionBootstrap.RegisterServices(builder, builder.Configuration);

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(typeof(GlobalExceptionFilter));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();


    }
}




