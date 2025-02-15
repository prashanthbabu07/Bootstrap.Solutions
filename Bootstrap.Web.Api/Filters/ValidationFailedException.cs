using System;
using System.Net;
using FluentValidation.Results;

namespace Bootstrap.Web.Api.Filters;

public class Error
    {
        public string? FieldName { get; set; }
        public string? Message { get; set; }
    }

    public class ErrorInfo
    {
        public IEnumerable<Error> Errors { get; set; } = new List<Error>();
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }


    public class ValidationFailedException : Exception
    {
        public ErrorInfo ErrorInfo { get; set; } = new ErrorInfo();

        public ValidationFailedException(IEnumerable<ValidationFailure> failures, int statusCode = (int)HttpStatusCode.BadRequest, string message = "Validations failed")
        {
            ErrorInfo.Errors = failures.Select(f => new Error { FieldName = f.PropertyName, Message = f.ErrorMessage });
            ErrorInfo.StatusCode = statusCode;
            ErrorInfo.Message = message;
        }

        public ValidationFailedException(string message, int statusCode) : base(message)
        {
            ErrorInfo.StatusCode = statusCode;
            ErrorInfo.Message = message;
        }
    }
