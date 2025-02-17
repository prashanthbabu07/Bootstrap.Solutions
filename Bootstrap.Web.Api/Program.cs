using Asp.Versioning;
using Autofac.Extensions.DependencyInjection;
using Bootstrap.Web.Api.Filters;
using Bootstrap.Web.Api.Infrastructure.IOC;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bootstrap.Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(typeof(GlobalExceptionFilter));
        });
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();


        // API Versioning
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            // options.ApiVersionReader = ApiVersionReader.Combine(
            //     new UrlSegmentApiVersionReader()
            //     // new QueryStringApiVersionReader("api-version"),
            //     // new HeaderApiVersionReader("X-Version"),
            //     // new MediaTypeApiVersionReader("X-Version")
            //     );
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // builder.Services.addpaie(options =>
        // {
        //     options.GroupNameFormat = "'v'VVV";
        //     options.SubstituteApiVersionInUrl = true;
        // });

        // swagger ui
        builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bootstrap API", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "Bootstrap API", Version = "v2" });
            // Add other versions as needed.

            // options.OperationFilter<SwaggerVersioningParameter>();

            // options.DocInclusionPredicate((version, desc) =>
            // {
            //     if (!desc.TryGetMethodInfo(out var methodInfo)) return false;

            //     var versions = methodInfo.DeclaringType?
            //         .GetCustomAttributes(true)
            //         .OfType<ApiVersionAttribute>()
            //         .SelectMany(attr => attr.Versions).ToList();

            //     var mappedVersions = desc.ActionDescriptor.EndpointMetadata
            //         .OfType<MapToApiVersionAttribute>()
            //         .SelectMany(attr => attr.Versions)
            //         .ToList();

            //     return versions?.Any(v => $"v{v.ToString()}" == version) == true
            //            || mappedVersions?.Any(v => $"v{v.ToString()}" == version) == true;
            // });

            // options.("api-version", new OpenApiParameter
            // {
            //     Name = "api-version",
            //     In = ParameterLocation.Path, // or ParameterLocation.Query if you use query parameter versioning
            //     Description = "API version to use",
            //     Required = true,
            //     Schema = new OpenApiSchema
            //     {
            //         Type = "string",
            //         Enum = new List<IOpenApiAny> { new OpenApiString("1.0"), new OpenApiString("2.0") } // Add all your versions here.
            //     }
            // });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

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
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Theorem Rhythm Web API v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Theorem Rhythm Web API v2");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();


    }
}




