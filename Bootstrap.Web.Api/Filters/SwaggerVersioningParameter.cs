using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bootstrap.Web.Api.Filters;

public class SwaggerVersioningParameter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		// Check if the path contains the version placeholder
		if (operation.Parameters == null)
			operation.Parameters = new List<OpenApiParameter>();

		// Add the version parameter to the path if it's missing
		operation.Parameters.Add(new OpenApiParameter
		{
			Name = "apiVersion",
			In = ParameterLocation.Path,
			Required = true,
			Description = "API version",
			Schema = new OpenApiSchema
			{
				Type = "string",
                Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("1.0"),
                    new OpenApiString("2.0")
                }
			}
		});
	}
}
