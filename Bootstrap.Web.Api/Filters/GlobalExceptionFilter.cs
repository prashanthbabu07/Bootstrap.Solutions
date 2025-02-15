using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bootstrap.Web.Api.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
	public override void OnException(ExceptionContext context)
	{
		HandleException(context).GetAwaiter().GetResult();
	}

	public override Task OnExceptionAsync(ExceptionContext context)
	{
		return HandleException(context);
	}

	protected Task HandleException(ExceptionContext context)
	{
		if (context.Exception is ValidationFailedException)
		{
			var validationException = context.Exception as ValidationFailedException;
			context.HttpContext.Response.ContentType = "application/json";
			context.HttpContext.Response.StatusCode = validationException.ErrorInfo.StatusCode;
			context.Result = new JsonResult(validationException.ErrorInfo);
			return Task.CompletedTask;
		}

		if (context.Exception is CustomErrorException)
		{
			var validationException = context.Exception as CustomErrorException;
			context.HttpContext.Response.ContentType = "application/json";
			context.HttpContext.Response.StatusCode = validationException.ErrorInfo.StatusCode;
			context.Result = new JsonResult(validationException.ErrorInfo);
			return Task.CompletedTask;
		}

		var status = HttpStatusCode.InternalServerError;
		context.HttpContext.Response.StatusCode = (int)status;
		context.Result = new JsonResult(new InternalError
		{
			Message = "Internal service error",
			InternalErrorMessage = context.Exception.StackTrace,
		});
		return Task.CompletedTask;
	}
}
