using FluentValidation;
using MediatR;

namespace Bootstrap.Web.Api.Filters;

public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Console.WriteLine($"Validator::{request.GetType().Name}");
        //Console.WriteLine(_validators.Count());
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
                        .Select(v => v.Validate(context))
                        .SelectMany(r => r.Errors)
                        .Where(f => f != null)
                        .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationFailedException(failures);
        }

        return next();
    }
}
